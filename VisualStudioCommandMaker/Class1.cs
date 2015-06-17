﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace Company.VisualStudioCommandMaker
{
    class Class1
    {
        public void Run(EnvDTE80.DTE2 DTE, Microsoft.VisualStudio.Shell.Package package)
        {
            EnvDTE.TextSelection ts = DTE.ActiveWindow.Selection as EnvDTE.TextSelection;
            if (ts == null)
                return;

            EnvDTE.CodeParameter codeParam = ts.ActivePoint.CodeElement[vsCMElement.vsCMElementParameter] as EnvDTE.CodeParameter;
            if (codeParam == null)
                return;

            System.Type tClass = GetTypeByName(DTE, package, codeParam.Type.AsFullName);
            string properties = "";
            foreach (var p in tClass.GetProperties())
            {
                properties += codeParam.Name + "." + p.Name + System.Environment.NewLine;
            }
            System.Windows.Clipboard.SetText(properties);
            System.Windows.MessageBox.Show(properties);
        }

        private System.Type GetTypeByName(EnvDTE80.DTE2 DTE, Microsoft.VisualStudio.Shell.Package package, string name)
        {
            System.IServiceProvider serviceProvider = package as System.IServiceProvider;
            var typeService =
                serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Design.DynamicTypeService)) as
                Microsoft.VisualStudio.Shell.Design.DynamicTypeService;

            Microsoft.VisualStudio.Shell.Interop.IVsSolution sln =
                serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.IVsSolution)) as
                Microsoft.VisualStudio.Shell.Interop.IVsSolution;

            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hier;
            sln.GetProjectOfUniqueName(DTE.ActiveDocument.ProjectItem.ContainingProject.UniqueName, out hier);

            return typeService.GetTypeResolutionService(hier).GetType(name, true);
        }
    }
}
