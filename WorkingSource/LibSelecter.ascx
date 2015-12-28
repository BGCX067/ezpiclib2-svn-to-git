<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LibSelecter.ascx.cs" Inherits="LibSelecter" %>
Lib:<asp:DropDownList ID="LibSelect" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="LibSelect_SelectedIndexChanged">
</asp:DropDownList>
<asp:Label ID="LibSelecterError" runat="server" ForeColor="Red"></asp:Label>
