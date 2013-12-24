<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KareKodUret.aspx.cs" Inherits="KareKodWeb.KareKodUret" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:Image ID="Image1" runat="server" /><br />
        <asp:TextBox ID="TextBox1" runat="server" Height="172px" TextMode="MultiLine" Width="265px"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="KareKodÜret" OnClick="Button1_Click" />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager><br />
        <asp:Label ID="Label1" runat="server" Text="Arka Renk"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:ColorPickerExtender ID="ColorPickerExtender1" TargetControlID="TextBox2" runat="server"></asp:ColorPickerExtender><br />
        <asp:Label ID="Label2" runat="server" Text="Rengi"></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:ColorPickerExtender ID="ColorPickerExtender2" TargetControlID="TextBox3" runat="server"></asp:ColorPickerExtender>
    </div>
    </form>
</body>
</html>
