<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TinyMCE.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: 'textarea',  // change this value according to your HTML
            plugin: 'a_tinymce_plugin',
            a_plugin_option: true,
            a_configuration_option: 400
        });
    </script>
    <title></title>
</head>
<body>
    <h1>TinyMCE Quick Start Guide</h1>
    <form method="post">
        <textarea id="mytextarea">Hello, World!</textarea>
    </form>
</body>
</html>
