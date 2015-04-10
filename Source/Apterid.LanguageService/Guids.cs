// Guids.cs
// MUST match guids.h
using System;

namespace Apterid.Apterid_LanguageService
{
    static class GuidList
    {
        public const string guidApterid_LanguageServicePkgString = "9360243e-b290-49c9-9ae4-008a5bb8a984";
        public const string guidApterid_LanguageServiceCmdSetString = "1264aca3-4f65-4745-9336-3f13698baf3b";

        public static readonly Guid guidApterid_LanguageServiceCmdSet = new Guid(guidApterid_LanguageServiceCmdSetString);
    };
}