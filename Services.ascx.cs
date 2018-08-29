﻿/*
'  DNN (formerly DotNetNuke) - http://www.dnnsoftware.com
'  Copyright (c) 2002-2018
'  by DNN Corp
' 
'  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
'  documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
'  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
'  to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
'  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
'  of the Software.
' 
'  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
'  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
'  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
'  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
'  DEALINGS IN THE SOFTWARE.
*/

using System;
using System.IO;

using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;

using DotNetNuke.Modules.Store.API;
using DotNetNuke.Modules.Store.Core.Admin;
using DotNetNuke.Modules.Store.Core.Components;

namespace DotNetNuke.Modules.Store.WebControls
{
    public partial class Services : StoreControlBase
    {
        #region Properties

        public string ScriptObjectName { get; set; }

        public string Options { get; set; }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleSettings settings = new ModuleSettings(ModuleId, TabId);
            StoreInfo storeInfo = StoreController.GetStoreInfo(PortalSettings.PortalId);

            if (settings != null && storeInfo != null)
            {
                string templatePath = storeInfo.PortalTemplates ? PortalSettings.HomeDirectory + "Store/Templates/" : TemplateSourceDirectory + "/../Templates/";
                templatePath += "Script.js";

                if (File.Exists(MapPath(templatePath)))
                {
                    ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                    ClientResourceManager.RegisterScript(Page, templatePath, FileOrder.Js.DefaultPriority, "DnnPageHeaderProvider");
                }

                ScriptObjectName = settings.ScriptObjectName;
                Options = settings.Options;
            }
        }

        #endregion
    }
}