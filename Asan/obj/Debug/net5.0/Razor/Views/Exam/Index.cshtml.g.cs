#pragma checksum "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5a9755efbc9e33f937469c2dfdfd9c8a440da731"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exam_Index), @"mvc.1.0.view", @"/Views/Exam/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a9755efbc9e33f937469c2dfdfd9c8a440da731", @"/Views/Exam/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccbd30536afbe90fbb634b64688aa02181cf1ae4", @"/Views/_ViewImports.cshtml")]
    public class Views_Exam_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Exam>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main style=""margin-bottom:100px;"">

    <div class=""container"">
        <div class=""row"">
            <div class=""col-md-12"">
                <h1 class=""mt-5"" style=""font-family: 'Noto Sans';
                font-style: normal;
                font-weight: 700;
                font-size: 24px;
                line-height: 28px;
                color: #383838;"">
                    Yoxlama suallar??
                </h1>
                <span class=""my-3""> ??m??k M??nasib??tl??rinin Monitorinqi M??rk??zi t??r??find??n h??yata ke??iril??n s??yyar monitorinql??r zaman?? a??a????dak?? yoxlama suallar??ndan istifad?? olunur:</span>
            </div>
");
#nullable restore
#line 21 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
             foreach (Exam exam in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""col-md-6"">
                    <div class=""box"" style="" background: #FFFFFF;
                box-shadow: 0px 3px 5px rgba(176, 190, 197, 0.32), 0px 8px 24px rgba(176, 190, 197, 0.32);
                border-radius: 10px;"">
                        <div class=""text-center my-5"">
                            ");
#nullable restore
#line 28 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
                       Write(Html.Raw(exam?.SvgFirst));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"                        </div>
                        <div>

                            <p class=""text-center"" style=""font-family: 'Noto Sans';
                    font-style: normal;
                    font-weight: 400;
                    font-size: 16px;
                    line-height: 28px;
                    text-align: center;
                    color: #383838;"">
                                ");
#nullable restore
#line 73 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
                           Write(exam?.TitleFirst);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"                            </p>
                        </div>
                    </div>

                </div>
                <div class=""col-md-6"">

                    <div class=""box"" style=""background: #FFFFFF;
             box-shadow: 0px 3px 5px rgba(176, 190, 197, 0.32), 0px 8px 24px rgba(176, 190, 197, 0.32);
             border-radius: 10px;"">
                        <div class=""text-center my-5"">
                            ");
#nullable restore
#line 87 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
                       Write(Html.Raw(exam?.SvgSecond));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"
                        </div>
                        <div>

                            <p class=""text-center"" style=""font-family: 'Noto Sans';
                    font-style: normal;
                    font-weight: 400;
                    font-size: 16px;
                    line-height: 28px;
                    text-align: center;
                    color: #383838;"">
                            ");
#nullable restore
#line 157 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
                       Write(exam?.TitleSecond);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("</p>\r\n                        </div>\r\n\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 163 "D:\AsanXidmet\Asan\Asan\Views\Exam\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</main>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Exam>> Html { get; private set; }
    }
}
#pragma warning restore 1591
