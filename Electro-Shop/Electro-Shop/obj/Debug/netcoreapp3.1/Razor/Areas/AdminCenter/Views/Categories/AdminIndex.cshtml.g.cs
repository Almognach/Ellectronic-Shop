#pragma checksum "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0372155c654d8f5ed882cc91cc30d5dc6320fb8a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminCenter_Views_Categories_AdminIndex), @"mvc.1.0.view", @"/Areas/AdminCenter/Views/Categories/AdminIndex.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0372155c654d8f5ed882cc91cc30d5dc6320fb8a", @"/Areas/AdminCenter/Views/Categories/AdminIndex.cshtml")]
    public class Areas_AdminCenter_Views_Categories_AdminIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Electro_Shop.Models.Category>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Index</h1>\n\n<p>\n    <a asp-action=\"Create\">Create New</a>\n</p>\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>\n                ");
#nullable restore
#line 17 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 23 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>\n                ");
#nullable restore
#line 26 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 592, "\"", 615, 1);
#nullable restore
#line 29 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
WriteAttributeValue("", 607, item.Id, 607, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a> |\n                <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 666, "\"", 689, 1);
#nullable restore
#line 30 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
WriteAttributeValue("", 681, item.Id, 681, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Delete</a>\n            </td>\n        </tr>\n");
#nullable restore
#line 33 "G:\פרוייקט\22-15 (15-11)\Electronic-Shop\Electro-Shop\Electro-Shop\Areas\AdminCenter\Views\Categories\AdminIndex.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Electro_Shop.Models.Category>> Html { get; private set; }
    }
}
#pragma warning restore 1591
