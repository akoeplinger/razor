﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Test.MyComponent<int>>(0);
            __builder.AddAttribute(1, "Item", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
                             3

#line default
#line hidden
#nullable disable
            ));
            __builder.SetKey(
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
                                      _someKey

#line default
#line hidden
#nullable disable
            );
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 3 "x:\dir\subdir\Test\TestComponent.cshtml"
       
    private object _someKey = new object();

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591