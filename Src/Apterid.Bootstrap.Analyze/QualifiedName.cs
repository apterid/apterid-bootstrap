// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class QualifiedName
    {
        string[] tokens;

        string[] qs = null;
        string n = null;
        string fn = null;
        int? h = null;

        public AssemblyName AssemblyName { get; set; }

        public ICollection<string> Qualifiers
        {
            get
            {
                if (qs != null)
                    return qs;

                if (tokens == null || tokens.Length == 0)
                    return null;

                return (qs = tokens.Take(tokens.Length - 1).ToArray());
            }
            internal set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Qualifiers));

                var name = tokens != null && tokens.Length > 1 
                    ? tokens[tokens.Length - 1] 
                    : null;
                var toks = value.ToArray();
                var size = toks.Length + 1;

                if (tokens == null || tokens.Length != size)
                    tokens = new string[size];

                for (int i = 0; i < toks.Length; i++)
                    tokens[i] = toks[i];
                tokens[tokens.Length - 1] = name;
                Clear();
            }
        }

        public string Name
        {
            get
            {
                if (n != null)
                    return n;

                if (tokens == null || tokens.Length == 0)
                    return null;

                return (n = tokens[tokens.Length - 1]);
            }
            internal set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Name));

                if (tokens == null || tokens.Length == 0)
                    tokens = new string[1];

                tokens[tokens.Length - 1] = value.Trim();
                Clear();
            }
        }

        public string FullName
        {
            get
            {
                if (fn != null)
                    return fn;

                if (tokens == null || tokens.Length == 0)
                    return null;

                return (fn = string.Join(".", tokens));
            }
            internal set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(FullName));

                tokens = value.Trim().Split('.');
                Clear();
            }
        }

        public ICollection<string> Tokens
        {
            get { return tokens; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Tokens));

                tokens = value.ToArray();
                if (tokens.Length == 0)
                    throw new ArgumentNullException(nameof(Tokens));
                Clear();
            }
        }

        public QualifiedName()
        {
        }

        public QualifiedName(IEnumerable<string> tokens)
        {
            Tokens = tokens.ToArray();
        }

        public QualifiedName(string fullName)
            : this(fullName.Split('.'))
        {
        }

        public QualifiedName(Scope parent, string name)
        {
            Qualifiers = parent.Name.Tokens;
            Name = name;
        }

        void Clear()
        {
            qs = null;
            n = null;
            fn = null;
            h = null;
        }

        public override bool Equals(object obj)
        {
            var other = obj as QualifiedName;
            if (other == null) return false;

            var equals = (AssemblyName == null && other.AssemblyName == null)
                || (AssemblyName != null && AssemblyName.Equals(other.AssemblyName));

            equals = equals
                && (((tokens == null || tokens.Length == 0) && (other.tokens == null || other.tokens.Length == 0))
                    || (tokens != null && tokens.SequenceEqual(other.tokens)));

            return equals;
        }

        public override int GetHashCode()
        {
            if (h != null)
                return h.Value;

            var hash = typeof(QualifiedName).GetHashCode();
            if (AssemblyName != null)
                hash ^= AssemblyName.GetHashCode();
            if (tokens != null)
                hash = tokens.Aggregate(hash, (h, t) => t != null ? h ^ t.GetHashCode() : h);

            h = hash;
            return hash;
        }

        public override string ToString()
        {
            var fn = FullName;
            return string.IsNullOrWhiteSpace(fn) ? "<empty>" : fn;
        }
    }
}
