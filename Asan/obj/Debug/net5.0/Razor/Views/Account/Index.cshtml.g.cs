#pragma checksum "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1a43f271bb7bb037ecb7affb994bf87b05feddee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Index), @"mvc.1.0.view", @"/Views/Account/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\AsanXidmet\Asan\Asan\Views\_ViewImports.cshtml"
using Asan;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AsanXidmet\Asan\Asan\Views\_ViewImports.cshtml"
using Asan.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\AsanXidmet\Asan\Asan\Views\_ViewImports.cshtml"
using Asan.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1a43f271bb7bb037ecb7affb994bf87b05feddee", @"/Views/Account/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccbd30536afbe90fbb634b64688aa02181cf1ae4", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Account>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("  justify-content: center;\r\n                    align-items: center;display: flex;    height: 90px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml"
  
    ViewData["Title"] = "Account";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main style=""margin-bottom:100px;"">

    <div class=""container"">
        <div class=""row"">

            <div class=""col-md-12 mt-5"">

                <h1 style=""font-family: 'Noto Sans';
                         font-style: normal;
                         font-weight: 700;
                         font-size: 24px;
                         line-height: 28px;
                         color: #383838;"">
                    Hesabat
                </h1>
            </div>


");
#nullable restore
#line 24 "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml"
             foreach (Account account in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""col-md-12 my-3 d-flex justify-content-cente"">
                    <div style=""height: 100px;width: 100%; background-color:#FFFFFF; border-radius:10px;"">
                        <div style=""width: 100px; height: 100px; background: linear-gradient(180deg, #00AAE2 0%, #007DA6 100%); float: right; border-top-right-radius: 10px;border-bottom-right-radius: 10px;"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1a43f271bb7bb037ecb7affb994bf87b05feddee5205", async() => {
                WriteLiteral(@"
                                <svg style=""    left: 30%;
                       "" width=""52"" height=""52"" viewBox=""0 0 52 52"" fill=""none""
                                     xmlns=""http://www.w3.org/2000/svg"">
                                    <path d=""M49.4 31.2C48.7104 31.2 48.0491 31.4739 47.5615 31.9615C47.0739 32.4491 46.8 33.1104 46.8 33.8V44.2C46.8 44.8896 46.5261 45.5509 46.0385 46.0385C45.5509 46.5261 44.8896 46.8 44.2 46.8H7.8C7.11044 46.8 6.44912 46.5261 5.96152 46.0385C5.47393 45.5509 5.2 44.8896 5.2 44.2V33.8C5.2 33.1104 4.92607 32.4491 4.43848 31.9615C3.95088 31.4739 3.28956 31.2 2.6 31.2C1.91044 31.2 1.24912 31.4739 0.761522 31.9615C0.273928 32.4491 0 33.1104 0 33.8V44.2C0 46.2687 0.821784 48.2526 2.28457 49.7154C3.74735 51.1782 5.73131 52 7.8 52H44.2C46.2687 52 48.2526 51.1782 49.7154 49.7154C51.1782 48.2526 52 46.2687 52 44.2V33.8C52 33.1104 51.7261 32.4491 51.2385 31.9615C50.7509 31.4739 50.0896 31.2 49.4 31.2ZM24.154 35.646C24.4013 35.8827 24.6928 36.0683 25.012 36.192C25.3232 36.3");
                WriteLiteral(@"296 25.6597 36.4006 26 36.4006C26.3403 36.4006 26.6768 36.3296 26.988 36.192C27.3072 36.0683 27.5987 35.8827 27.846 35.646L38.246 25.246C38.7356 24.7564 39.0106 24.0924 39.0106 23.4C39.0106 22.7076 38.7356 22.0436 38.246 21.554C37.7564 21.0644 37.0924 20.7894 36.4 20.7894C35.7076 20.7894 35.0436 21.0644 34.554 21.554L28.6 27.534V2.6C28.6 1.91044 28.3261 1.24912 27.8385 0.761522C27.3509 0.273928 26.6896 0 26 0C25.3104 0 24.6491 0.273928 24.1615 0.761522C23.6739 1.24912 23.4 1.91044 23.4 2.6V27.534L17.446 21.554C17.2036 21.3116 16.9158 21.1193 16.599 20.9881C16.2823 20.8569 15.9428 20.7894 15.6 20.7894C15.2572 20.7894 14.9177 20.8569 14.601 20.9881C14.2842 21.1193 13.9964 21.3116 13.754 21.554C13.5116 21.7964 13.3193 22.0842 13.1881 22.4009C13.0569 22.7177 12.9894 23.0572 12.9894 23.4C12.9894 23.7428 13.0569 24.0823 13.1881 24.399C13.3193 24.7158 13.5116 25.0036 13.754 25.246L24.154 35.646Z""
                                          fill=""white"" />
                                </svg>

                   ");
                WriteLiteral("         ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml"
                                                       WriteLiteral(account.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </div>
                        <h2 style=""font-family: 'Noto Sans';
                  font-style: normal;
                  font-weight: 500;
                  font-size: 16px;
                  line-height: 22px;
                  width: 49%;
                  margin-left: 10px;
                  margin-top: 36px;
                  color: #383838;"">
                            ");
#nullable restore
#line 49 "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml"
                       Write(account.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </h2>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 53 "D:\AsanXidmet\Asan\Asan\Views\Account\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</main>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Account>> Html { get; private set; }
    }
}
#pragma warning restore 1591