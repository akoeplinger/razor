﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
using N;

#line default
#line hidden
#nullable disable
    public partial class TestComponent<
#nullable restore
#line 2 "x:\dir\subdir\Test\TestComponent.cshtml"
TParam

#line default
#line hidden
#nullable disable
    > : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            global::__Blazor.Test.TestComponent.TypeInference.CreateTestComponent_0(__builder, 0, 1, 
#nullable restore
#line 12 "x:\dir\subdir\Test\TestComponent.cshtml"
                           1

#line default
#line hidden
#nullable disable
            , 2, (context) => (__builder2) => {
#nullable restore
#line (14,9)-(14,29) 25 "x:\dir\subdir\Test\TestComponent.cshtml"
__builder2.AddContent(3, context.I1.MyClassId);

#line default
#line hidden
#nullable disable
                __builder2.AddContent(4, " - ");
#nullable restore
#line (14,33)-(14,54) 25 "x:\dir\subdir\Test\TestComponent.cshtml"
__builder2.AddContent(5, context.I2.MyStructId);

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
#nullable restore
#line 4 "x:\dir\subdir\Test\TestComponent.cshtml"
       
    [Parameter]
    public TParam InferParam { get; set; }

    [Parameter]
    public RenderFragment<(MyClass I1, MyStruct I2, TParam P)> Template { get; set; }

#line default
#line hidden
#nullable disable
    }
}
namespace __Blazor.Test.TestComponent
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateTestComponent_0<TParam>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TParam __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment<(global::N.MyClass I1, global::N.MyStruct I2, TParam P)> __arg1)
        {
        __builder.OpenComponent<global::Test.TestComponent<TParam>>(seq);
        __builder.AddComponentParameter(__seq0, "InferParam", __arg0);
        __builder.AddComponentParameter(__seq1, "Template", __arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
