<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TinyMCE.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>   
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-form.js"></script>
    <%--条件注释--%>
    <!--[if lte IE 8]> 
        <script type="text/ecmascript" src="js/respond.js"></script>
    <![endif]-->
    <script type="text/javascript" src="tinymce.min.js"></script>
        <script>
            //tinymce.init({
                //selector: 'textarea',
                //inline: true,
                //plugin: 'a_tinymce_plugin',
                //plugins : 'advlist autolink link image lists charmap print preview',
                //a_plugin_option: true,
                //a_configuration_option: 400,
                //toolbar: 'undo redo | styleselect | bold italic | link image'
                //toolbar: false
                //toolbar: [
                //    'undo redo | styleselect | bold italic | link image',
                //    'alignleft aligncenter alignright'
                //],
                //toolbar: false,
                //menubar: 'file edit view',
                //menubar: false,
                //menu: {
                //    view: { title: 'Edit', items: 'cut, copy, paste' }
                //}
                //menu: {
                //    view: { title: 'Happy', items: 'code' }
                //},
                //plugins: 'code'
                /*********************************************************/
                //selector: '#mytextarea',
                //theme: 'modern',
                //width: 600,
                //height: 300,
                //plugins: [
                //  'advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker',
                //  'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                //  'save table contextmenu directionality emoticons template paste textcolor'
                //],
                //content_css: 'css/content.css',
                //toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons'

                //selector: '.myeditablediv',
                //inline: true
            //});

            //tinymce.init({
            //    selector: 'textarea',
            //    height: 500,
            //    plugins: 'table',
            //    style_formats: [
            //      { title: 'Bold text', inline: 'strong' },
            //      { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
            //      { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
            //      { title: 'Badge', inline: 'span', styles: { display: 'inline-block', border: '1px solid #2276d2', 'border-radius': '5px', padding: '2px 5px', margin: '0 2px', color: '#2276d2' } },
            //      { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
            //    ],
            //    formats: {
            //        alignleft: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'left' },
            //        aligncenter: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'center' },
            //        alignright: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'right' },
            //        alignfull: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'full' },
            //        bold: { inline: 'span', 'classes': 'bold' },
            //        italic: { inline: 'span', 'classes': 'italic' },
            //        underline: { inline: 'span', 'classes': 'underline', exact: true },
            //        strikethrough: { inline: 'del' },
            //        customformat: { inline: 'span', styles: { color: '#00ff00', fontSize: '20px' }, attributes: { title: 'My custom format' }, classes: 'example1' },
            //    },
            //    content_css: [
            //      //'//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
            //      //'//www.tinymce.com/css/codepen.min.css'
            //    ]
            //});
    </script>

    <script type="text/javascript">
        tinymce.init({
            selector: 'textarea',
            height:500,
            theme: 'modern',
            //theme: 'advanced',
            editor_selector : "wysiwyg",
            language: 'zh_CN',
            language_url: 'js/zh_CN.js',
            plugins: [
              'advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker',
              'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
              'save table contextmenu directionality emoticons template paste textcolor textpattern example'
            ],
            //jbimages
            //theme_advanced_buttons1: "imagetools",
            //toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image textcolor",
            toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons',
            file_browser_callback: function (field_name, url, type, win) {
                if (type == 'image') $('#image_form input').click();
            }
        });
    </script>
    <title></title>
</head>
<body>
    <h1>TinyMCE Quick Start Guide</h1>
    <form method="post" action="handler.ashx" id="textareaForm">
        <textarea id="mytextarea" name="mytextarea">Hello, World!</textarea>

        <input type="submit" value="提交" />
    </form>

    <iframe id="form_target" name="form_target" style="display:none"></iframe>

    <form id="image_form" action="uploadImage.ashx" target="form_target" method="post" enctype="multipart/form-data" style="width:0px;height:0;overflow:hidden">
        <input name="image" type="file" onchange="$('#image_form').submit();" />
    </form>
    <script>
        $('#image_form').ajaxForm(function (data) {
            if (data != '0') {
                $('.mce-textbox').eq(0).val(data).attr({
                    'disabled': true
                });
            }
        });

        $('#textareaForm').submit(function (event) {
            event.preventDefault();
            tinyMCE.triggerSave();
            var formData = $(this).serialize();
            var responseData = {
                formData : formData
            };
            formData = null;
            var url = "handler.ashx";
            $.post(url, responseData, function (data) {
                alert(data);
            });
        });
    </script>
</body>
</html>
