<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginPage" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Login.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates.Controls" TagPrefix="tc" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Logon</title>
    <style>
        .LogonTemplate .LogonContent {
            padding: 0;
        }
        .WelcomeGroupWrapperCssClass {
            width: 100% !important;
        }
        .WelcomeGroupClassCSS {
            position: relative;
        }
        .LogonTemplate .GroupContent.lastEditorContainer .WelcomeTextClassCSS {
            background-color: #2c86d3;
            text-align: center;
            padding: 25px 120px;
            font-size: 1.4em;
        }
            .LogonTemplate .GroupContent.lastEditorContainer .WelcomeTextClassCSS .StaticText {
                color: #fff;
            }
        .LogonTemplate .GroupContent.lastEditorContainer .PasswordHintClassCSS {
            position: absolute;
            background: #feb71e;
            line-height: 1.1em;
            border-radius: 78px;
            height: 120px;
            width: 120px;
            box-sizing: border-box;
            text-align: center;
            padding: 30px 12px 18px;
            right: -53px;
            bottom: -90px;
        }
            .LogonTemplate .GroupContent.lastEditorContainer .PasswordHintClassCSS .StaticText {
                font-weight: bold;
                font-size: 0.85em;
                color: #fff;
            }
        .LogonTemplate .GroupContent.lastEditorContainer .LogonTextClassCSS {
            text-align: center;
            padding-right: 45px;
            padding-left: 45px;
            padding-top: 30px;
            padding-bottom: 30px;
        }
            .LogonTemplate .GroupContent.lastEditorContainer .LogonTextClassCSS .StaticText {
                font-size: 1em;
                color: #9a9a9a;
            }
        .MainGroupClassCSS {
            padding: 0 75px;
        }
        .dxmLite_XafTheme .dxm-main.menuButtons {
            padding: 10px 75px 80px;
        }
        @media (max-width: 600px), (max-height: 600px) {
            .LogonTemplate .GroupContent.lastEditorContainer .PasswordHintClassCSS {
                right: 0;
            }
            .LogonTemplate .GroupContent.lastEditorContainer .LogonTextClassCSS {
                padding-right: 45px;
                padding-left: 45px;
            }
        }
        @media (max-width: 480px), (max-height: 480px) {
            .LogonTemplate .GroupContent.lastEditorContainer .LogonContent {
                padding: 0;
            }
            .LogonTemplate .GroupContent.lastEditorContainer .WelcomeTextClassCSS {
                padding: 25px 108px;
            }
        }
    </style>
</head>
<body class="Dialog">
    <div id="PageContent" class="PageContent DialogPageContent">
        <form id="form1" runat="server">
            <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
            <div id="Content" runat="server" />
        </form>
    </div>
</body>
</html>
