﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apterid.Bootstrap.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Apterid.Bootstrap.Common.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate source file &quot;{0}&quot;..
        /// </summary>
        public static string EA_0009_Analyzer_DuplicateSourceFile {
            get {
                return ResourceManager.GetString("EA_0009_Analyzer_DuplicateSourceFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source file does not exist: {0}..
        /// </summary>
        public static string EB_0006_Compiler_SourceDoesNotExist {
            get {
                return ResourceManager.GetString("EB_0006_Compiler_SourceDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must specify an output path..
        /// </summary>
        public static string EC_0004_CmdLine_NoOutputPath {
            get {
                return ResourceManager.GetString("EC_0004_CmdLine_NoOutputPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown compiler output mode &apos;{0}&apos;..
        /// </summary>
        public static string EC_0005_CmdLine_UnknownOutputMode {
            get {
                return ResourceManager.GetString("EC_0005_CmdLine_UnknownOutputMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate output file &quot;{0}&quot;..
        /// </summary>
        public static string EC_0008_Compiler_DuplicateOutputFileInfo {
            get {
                return ResourceManager.GetString("EC_0008_Compiler_DuplicateOutputFileInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A node constructor&apos;s NodeArgs parameter must be named &quot;args&quot;..
        /// </summary>
        public static string EI_0001_ParserImpl_MakeNodeArgs {
            get {
                return ResourceManager.GetString("EI_0001_ParserImpl_MakeNodeArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A node constructor&apos;s Node[] parameter must be named &quot;children&quot;..
        /// </summary>
        public static string EI_0002_ParserImpl_MakeNodeChildren {
            get {
                return ResourceManager.GetString("EI_0002_ParserImpl_MakeNodeChildren", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find a constructor for {0}..
        /// </summary>
        public static string EI_0003_ParserImpl_MakeNodeNoCtor {
            get {
                return ResourceManager.GetString("EI_0003_ParserImpl_MakeNodeNoCtor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to parse..
        /// </summary>
        public static string EP_0007_Parser_SyntaxError {
            get {
                return ResourceManager.GetString("EP_0007_Parser_SyntaxError", resourceCulture);
            }
        }
    }
}
