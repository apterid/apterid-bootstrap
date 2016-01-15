using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Analyze.Abstract;

namespace Apterid.Bootstrap.Analyze
{
    public abstract class TypeResolver
    {
        public abstract AType ResolveType(QualifiedName name);
    }

    class ScopeTypeResolver : TypeResolver
    {
        ReferenceTypeResolver RefResolver { get; }

        public ScopeTypeResolver(ReferenceTypeResolver rtr)
        {
            RefResolver = rtr;
        }

        public override AType ResolveType(QualifiedName name)
        {
            return RefResolver.ResolveType(name);
        }
    }

    class ReferenceTypeResolver : TypeResolver
    {
        public ICollection<Reference> References { get; }

        IDictionary<QualifiedName, Scope> resolvedNamespaces = new Dictionary<QualifiedName, Scope>();
        IDictionary<QualifiedName, AType> resolvedTypes = new Dictionary<QualifiedName, AType>();

        public ReferenceTypeResolver(ICollection<Reference> references)
        {
            References = references;
        }

        object resolveLock = new object();

        public override AType ResolveType(QualifiedName name)
        {
            AType result;
            if (resolvedTypes.TryGetValue(name, out result))
                return result;

            lock (resolveLock)
            {
                if (resolvedTypes.TryGetValue(name, out result))
                    return result;

                // find and make type
                var refAndType = References
                    .Select(r => Tuple.Create(r, r.Assembly.GetType(name.FullName, false, false)))
                    .FirstOrDefault(t => t.Item2 != null);

                if (refAndType == null || refAndType.Item2 == null)
                    return null;

                result = new AType(null)
                {
                    Name = new QualifiedName(name.Tokens)
                    {
                        AssemblyName = refAndType.Item1.Assembly.GetName(),
                    },
                    CLRType = refAndType.Item2,
                };

                resolvedTypes.Add(name, result);

                // now fill in parent namespaces
                Scope childNamespace = result;
                for (int num = name.Tokens.Count - 1; num > 0; num--)
                {
                    var parentName = new QualifiedName(name.Tokens.Take(num));
                    Scope parentNamespace;
                    if (!resolvedNamespaces.TryGetValue(parentName, out parentNamespace))
                    {
                        parentNamespace = new Scope(null)
                        {
                            Name = parentName,
                        };
                        resolvedNamespaces.Add(parentName, parentNamespace);
                    }

                    if (childNamespace != null)
                    {
                        childNamespace.Parent = parentNamespace;
                        parentNamespace.Children.Add(childNamespace.Name, childNamespace);
                    }

                    childNamespace = parentNamespace;
                }

                return result;
            }
        }
    }
}
