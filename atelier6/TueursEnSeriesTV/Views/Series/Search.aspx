<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Séries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row">
        <div class="span12">
            <h1>Résultats</h1>
        </div>
        <div id="thumbnails">
        <% foreach (var serie in (IEnumerable<TueursEnSeries.Models.Serie>)ViewData["series"])
           { %>
           <div class="item well">
           <% if (serie.Banner.Length>0)
              { %>
             <a href="http://www.imdb.com/title/<%= HttpUtility.UrlEncode(serie.Imdb) %>"><img 
                src="http://thetvdb.com/banners/_cache/<%= serie.Banner %>" 
                title="<%= HttpUtility.HtmlAttributeEncode(serie.Name) %>" 
                alt="<%= HttpUtility.HtmlAttributeEncode(serie.Name) %>"/></a>
           <% } %>
             <div>
                <h4><%: serie.Name %></h4>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam ultrices velit a enim sagittis quis aliquet ante facilisis. Integer lacinia lorem auctor risus condimentum ac varius ante vulputate. Sed et mauris orci, nec bibendum metus. Suspendisse mauris sem, tincidunt id placerat ornare, ullamcorper vel ante.</p>
                <!-- <%: serie.Id %> -->
             </div>
           </div>
        <% } %>
        </div>
      </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="../../Scripts/jquery.grid-a-licious.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#thumbnails").gridalicious({ width: 225 });
        });
    </script>
</asp:Content>
