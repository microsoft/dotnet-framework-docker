<%@ Import Namespace="System.Runtime.InteropServices" %>
<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="aspnetapp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div align="center">
        <table class="table table-striped table-hover">
            <tr>
                <td>.NET version</td>
                <td><%= RuntimeInformation.FrameworkDescription %></td>
            </tr>
            <tr>
                <td>Operating system</td>
                <td><%= RuntimeInformation.OSDescription %></td>
            </tr>
            <tr>
                <td>Processor architecture</td>
                <td><%= RuntimeInformation.OSArchitecture %></td>
            </tr>
            <tr>
                <td>CPU cores</td>
                <td><%= Environment.ProcessorCount %></td>
            </tr>
            <tr>
                <td>DOTNET_RUNNING_IN_CONTAINER</td>
                <td><%= (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") is null ? "false" : "true") %></td>
            </tr>
        </table>
    </div>
</asp:Content>
