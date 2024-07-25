using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Language
    {
        public int Language_Id { get; set; }
        public string Language_Code { get; set; }
        public string LanguageName { get; set; }
        public string LanguageText { get; set; }

    }

    public class LanguageJsonJResponse
    {
        public List<Language> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }


    public class MemberLanguage {
        public long FK_MemId { get; set; }
        public string Language_Code { get; set; }
    }

    public class MemberLanguageResponse
    {
        public List<MemberLanguageResponse> result { get; set; }
        public int flag { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
       
    }
}