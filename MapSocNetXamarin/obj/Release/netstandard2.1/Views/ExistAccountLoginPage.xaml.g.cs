//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("MapSocNetXamarin.Views.ExistAccountLoginPage.xaml", "Views/ExistAccountLoginPage.xaml", typeof(global::MapSocNetXamarin.Views.ExistAccountLoginPage))]

namespace MapSocNetXamarin.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views/ExistAccountLoginPage.xaml")]
    public partial class ExistAccountLoginPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MapSocNetXamarin.ViewModels.CustomEntry MailInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MapSocNetXamarin.ViewModels.CustomEntry PassInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button LoginButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button RestorePassButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button OpenLoginPage;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ExistAccountLoginPage));
            MailInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MapSocNetXamarin.ViewModels.CustomEntry>(this, "MailInput");
            PassInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MapSocNetXamarin.ViewModels.CustomEntry>(this, "PassInput");
            LoginButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "LoginButton");
            RestorePassButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "RestorePassButton");
            OpenLoginPage = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "OpenLoginPage");
        }
    }
}
