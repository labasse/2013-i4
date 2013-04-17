<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Recherche
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="hero-unit">
        <h1>Tueurs en séries <sup>TV</sup></h1>
        <p>Toutes les infos sur vos séries préférées.</p>
        <% using (Html.BeginForm("Search", "Series", new { @class="form-search" }))
           { %>
            <input id='q' name='q' type='text' placeholder='Titre de la série' class='search-query input-large'/>
            <button type='submit' class='btn btn-primary' >Chercher</button>
        <% } %>
      </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $('#q').focus();
    </script>
</asp:Content>
