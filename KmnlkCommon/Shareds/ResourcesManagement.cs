using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Collections;

namespace KmnlkCommon.Shareds
{
    public class ResourcesManagement
    {
        public enum ResourceLang
        {
            AR,
            EN,
            FR
        }

        private static ResourceManager englishResourceContainer;
        private static ResourceManager arabicResourceContainer;
        private static ResourceManager franchResourceContainer;

        private static CultureInfo englishCultureType;
        private static CultureInfo arabicCultureType;
        private static CultureInfo franchCultureType;

        private static string FILE_EN_RESOURCES = "KmnlkResources.en";
        private static string FILE_AR_RESOURCES = "KmnlkResources.ar";
        private static string FILE_FR_RESOURCES = "KmnlkResources.fr";

        private static string pathFile="";
        private static bool isFirstTime = true;
        public static void setConfiguration(string path,string pathJS="")
        {
            pathFile = path;
            init();
            if(pathJS!="")
            generateResourcesFile(pathJS);
        }
        private static void init() {
            englishResourceContainer = ResourceManager.CreateFileBasedResourceManager(FILE_EN_RESOURCES, pathFile, null);
            arabicResourceContainer = ResourceManager.CreateFileBasedResourceManager(FILE_AR_RESOURCES, pathFile, null);
            franchResourceContainer = ResourceManager.CreateFileBasedResourceManager(FILE_FR_RESOURCES, pathFile, null);
            englishCultureType = new CultureInfo("en");
            arabicCultureType = new CultureInfo("ar");
            franchCultureType = new CultureInfo("fr");

        }
        public static string getResourceValue(string key, string defaultValue, ResourceLang lang)
        {
            if (isFirstTime)
            {
                init();
                isFirstTime = false;
            }
            if (pathFile == "" || key == "")
                return  defaultValue;

            string value = "";
            if (lang == ResourceLang.AR)
            {
                value = arabicResourceContainer.GetString(key, arabicCultureType);
            }else if (lang == ResourceLang.EN)
            {
                value = englishResourceContainer.GetString(key, englishCultureType);
            }
            else if(lang==ResourceLang.FR){
                value = franchResourceContainer.GetString(key, franchCultureType);
            }

            if (value == null)
                value = defaultValue;

            if (value == "")
                value = key;

            return value;
        }
        private static void generateResourcesFile(string path)
        {
            try
            {
                StringBuilder st = new StringBuilder();
                if (File.Exists(path))
                {
                    st.Append("var aryResources = new Map();");
                    st.Append("\n\n");
                    foreach (DictionaryEntry entry in englishResourceContainer.GetResourceSet(englishCultureType, true, true))
                    {
                        String strKey = entry.Key.ToString();
                        String strValueEn = englishResourceContainer.GetString(entry.Key.ToString(), englishCultureType);
                        String strValueAr = arabicResourceContainer.GetString(entry.Key.ToString(), arabicCultureType);
                        if(strValueAr==null || strValueAr == "")
                        {
                            strValueAr = strValueEn;
                        }
                       
                        String strValueFr = franchResourceContainer.GetString(entry.Key.ToString(), franchCultureType);
                        if (strValueFr == null || strValueFr == "")
                        {
                            strValueFr = strValueEn;
                        }
                        st.Append("aryResources.set('" + strKey + "', { key: '" + strKey + "', value: '" + strValueEn + "', ar: '" + strValueAr + "', en: '" + strValueEn + "', fr: '" + strValueFr + "', type: '" + strValueEn + "' }); ");
                    }

                    st.Append("\n\n\n\n\n\n");
                    st.Append(@"function getResourceValue(key,lang) {
             if (typeof(aryResources) == 'undefined')
                {
                    return key;
                }
                var res = filterResources(key);
             if (res != false && res!=null)
                {
                    if (lang == ""0"")
                    {
                        return res.en;
                    }
                    else if (lang == ""1"")
                    {
                        return res.ar;
                    }
                    else
                    {
                        return key;
                    }
                }
                else
                {
                    return key;
                }
            }
            ");
                    st.Append("\n\n\n\n\n\n");
                    st.Append(@"
function getResource(key) {
    if (typeof (aryResources) == 'undefined') {
        return key;
    }
    var res =filterResources(key);
        return res;

}
");

                    st.Append("\n\n\n\n\n\n");
                    st.Append(@"function filterResources(key) {
    return aryResources.get(key);
}
");


                    StreamWriter wr = File.CreateText(path);
                    wr.Write(st.ToString());
                    wr.Close();
                }
            }catch(Exception e)
            {

            }
        }
    }
}
