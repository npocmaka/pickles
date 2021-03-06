﻿#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using Ninject;
using Ninject.Modules;
using Pickles.DocumentationBuilders.DITA;
using Pickles.DocumentationBuilders.Excel;
using Pickles.DocumentationBuilders.HTML;
using Pickles.DocumentationBuilders.JSON;
using Pickles.DocumentationBuilders.Word;
using Pickles.TestFrameworks;

namespace Pickles
{
    public class PicklesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Configuration>().ToSelf().InSingletonScope();

            Bind<IDocumentationBuilder>().To<HtmlDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Html).InSingletonScope();
            Bind<IDocumentationBuilder>().To<WordDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Word).InSingletonScope();
            Bind<IDocumentationBuilder>().To<DitaDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Dita).InSingletonScope();
            Bind<IDocumentationBuilder>().To<JSONDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.JSON).InSingletonScope();
            Bind<IDocumentationBuilder>().To<ExcelDocumentationBuilder>().When(request => Kernel.Get<Configuration>().DocumentationFormat == DocumentationFormat.Excel).InSingletonScope();

            Bind<ITestResults>().To<NullTestResults>().When(request => !Kernel.Get<Configuration>().HasTestResults).InSingletonScope();
            Bind<ITestResults>().To<NUnitResults>().When(request =>
                Kernel.Get<Configuration>().HasTestResults &&
                Kernel.Get<Configuration>().TestResultsFormat == TestResultsFormat.NUnit).InSingletonScope();
            Bind<ITestResults>().To<XUnitResults>().When(request =>
                Kernel.Get<Configuration>().HasTestResults &&
                Kernel.Get<Configuration>().TestResultsFormat == TestResultsFormat.xUnit).InSingletonScope();
            Bind<ITestResults>().To<MsTestResults>().When(request =>
                Kernel.Get<Configuration>().HasTestResults &&
                Kernel.Get<Configuration>().TestResultsFormat == TestResultsFormat.MsTest).InSingletonScope();

            Bind<LanguageServices>().ToSelf().InSingletonScope();
            Bind<HtmlTableOfContentsFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlFooterFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlDocumentFormatter>().ToSelf().InSingletonScope();
            Bind<IHtmlFeatureFormatter>().To<HtmlFeatureFormatter>().InSingletonScope();
            Bind<HtmlScenarioFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlStepFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlTableFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlMultilineStringFormatter>().ToSelf().InSingletonScope();
            Bind<HtmlDescriptionFormatter>().ToSelf().InSingletonScope();

            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = true;
            Bind<MarkdownDeep.Markdown>().ToConstant(markdown).InSingletonScope();
        }
    }
}