<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
    <style>
        @media (max-width: 550px) {
            #Vertical_LogoLink {
                display: none;
            }
        }
        table.CardGroupBase.SQLAlternativeInfoLayout {
            border: none;
        }
        table.CardGroupBase.SQLAlternativeInfoLayout .CardGroupContent {
            padding-top: 0px;
            padding-left: 20px;
            padding-right: 20px;
            padding-bottom: 0px;
        }
        table.CardGroupBase.SQLAlternativeInfoLayout .StaticText {
            color: #4a4a4a;
        }
    </style>
</head>
<body class="VerticalTemplate">
    <form id="form2" runat="server">
    <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
    <div runat="server" id="Content" />
    </form>
</body>
</html>
