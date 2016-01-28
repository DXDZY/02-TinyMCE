/**
 * Created by XiaoDong on 2015/11/25.
 */
//加载页面
(function($){
    $(document).ready(function () {
        //$('#mytextarea').html('<p>123</p>');
        $('#image_form').ajaxForm(function (data) {
            if (data != '0') {
                $('.mce-textbox').eq(0).val(data).attr({
                    'disabled': true
                });
            }
        });
        $('input[data-submit=submit]').click(function (event) {
            //$('#menuForm')
            //        .data('bootstrapValidator')
            //        .updateStatus('secondMenuLevelName', 'NOT_VALIDATED')
            //        .validateField('secondMenuLevelName');
            var $this = $(this);
            tinyMCE.triggerSave();
            var formData = $('#textareaForm').serialize();
            var submitType = 0;
            if ($this.val() == '发布') {
                submitType = 1;
            } else if ($this.val() == '存为草稿') {
                submitType = 0;
            }
            var secondMenuLevelName = $('[name=secondMenuLevelName]').attr('data-menuid');
            var secondMenuName = $('[name=secondMenuName]').attr('data-menuid');
            var title = $('#contentTitle').val();
            var responseData = {
                formData: formData,
                submitType: submitType,
                secondMenuLevelName:secondMenuLevelName,
                secondMenuName: secondMenuName,
                title: title
            };
            formData = null;
            var url = "handler.ashx";
            $.post(url, responseData, function (data) {
                alert(data);
            });
            //tinyMCE.setContent('123');
        });
        //form校验
        $('#menuForm').bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                secondMenuLevelName: {
                    validators: {
                        notEmpty: {
                            message: '一级菜单不能为空'
                        }
                    }
                }
            }
        });
        //同步
        $.ajaxSetup({
            async : false
        });
        //加载导航条
        if ($('#menu').length > 0) {
            var url = 'handler/GetDataHandler.ashx';
            var requestData = {
                cmd : 'getMenu',
                userPower : '0'
            };
            $.get(url, requestData, function (data) {
                if (data.length > 0) {
                    var dataJson = $.parseJSON(data);
                    $('#menu').menu({
                        data: dataJson
                    });
                }
            });
            //$('#menu').load('dropdown-menu.aspx #menu-col');
        }
        //菜单后台一级菜单下拉值初始化
        if ($('#first-Menu-Drop-down,#second-Menu-Drop-down').length > 0) {
            var url = 'handler/GetDataHandler.ashx';
            var requestData = {
                cmd: 'getMenu',
                userPower: '0'
            };
            $.get(url, requestData, function (data) {
                if (data.length > 0) {
                    var dataJson = $.parseJSON(data);
                    var html = '';
                    var htmlOrder = '';
                    $.each(dataJson, function (index, item) {
                        if (index == 0) {
                            return true;
                        }
                        if (index == 1) {
                            html += '<div class="input-group"><div class="input-group-btn"><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
                            html += item.menu_cn_name;
                            html += '<span class="caret"></span></button><ul class="dropdown-menu">';
                        }                        
                        html += '<li><a style="cursor:pointer;" data-menuPower="' + item.menu_power + '" data-menuUrl="' + item.menu_url + '" data-parentID="' + item.menu_parent_id + '" data-menuID="' + item.menu_id + '" freeze="' + item.freeze + '">' + item.menu_cn_name + '</a></li>';
                        //初始化一级菜单排序控件
                        htmlOrder += '<li class="ui-state-default" data-parentID="' + item.menu_parent_id + '">' + item.menu_cn_name + '</li>';
                    });
                    if (htmlOrder != '') {
                        $('#fMenuOrder').show('slow');
                    }
                    var firstHtml = html + '</ul></div><input type="text"class="form-control" name="firstMenuName" id="firstMenuName"/></div>';
                    var secondHtml = html + '</ul></div><input type="text"class="form-control" name="secondMenuLevelName" readonly/></div>';
                    $('#first-Menu-Drop-down').html(firstHtml);
                    $('#second-Menu-Drop-down').html(secondHtml);
                    //初始化一级菜单排序控件
                    $('#sortableF').html(htmlOrder);
                    $('#sortableF').hide();
                    $('#sortableF').show('slow');
                }

            });            
        }
        //刷新页面
        $('#refresh').click(function () {
            window.location.reload();
        });
    });
})(jQuery);

//添加事件,及页面加在后添加数据
(function ($) {
    $(document).ready(function () {        
        //菜单后台一级菜单下拉点击事件
        $(document).on('click', '.dropdown-menu a', function (event) {
            var $this = $(this);
            var $text = $this.text();
            var html = $text;
            html += '<span class="caret"></span>'
            $this.closest('ul').prev().html(html);
            var $currentInput = $this.closest('.input-group-btn').next();
            $currentInput.val($text).focus().attr('data-menuID', $this.attr('data-menuID'));
            //操作一级菜单
            if ($currentInput.attr('name') == 'firstMenuName') {
                //验证菜单名称
                $('#defaultFormF')
                    .data('bootstrapValidator')
                    .updateStatus('firstMenuName', 'NOT_VALIDATED')
                    .validateField('firstMenuName');
                //权限赋值
                $('#firstMenuPower').val($this.attr('data-menuPower'));
                //url赋值
                $('#firstMenuNameUrl').val($this.attr('data-menuUrl'));
                //是否冻结赋值
                if ($this.attr('freeze') == '1') {
                    $('#freezeFirstMenu').prop('checked', 'true');
                } else {
                    $('#freezeFirstMenu').removeAttr('checked');
                }
                //验证权限
                $('#defaultFormF')
                    .data('bootstrapValidator')
                    .updateStatus('firstMenuPower', 'NOT_VALIDATED')
                    .validateField('firstMenuPower');
            } else if ($currentInput.attr('name') == 'firstMenuPower') {
                //验证权限
                $('#defaultFormF')
                    .data('bootstrapValidator')
                    .updateStatus('firstMenuPower', 'NOT_VALIDATED')
                    .validateField('firstMenuPower');
            }
            if ($currentInput.attr('name') == 'secondMenuName') {
                //验证菜单名称
                //$('#defaultFormS')
                //    .data('bootstrapValidator')
                //    .updateStatus('secondMenuName', 'NOT_VALIDATED')
                //    .validateField('secondMenuName');
                //权限赋值
                //$('#secondMenuPower').val($this.attr('data-menuPower'));
                //url赋值
                //$('#secondMenuNameUrl').val($this.attr('data-menuUrl'));
                //是否冻结赋值
                //if ($this.attr('freeze') == '1') {
                //    $('#freezeSecondMenu').prop('checked', 'true');
                //} else {
                //    $('#freezeSecondMenu').removeAttr('checked');
                //}
                //验证权限
                //$('#defaultFormS')
                //    .data('bootstrapValidator')
                //    .updateStatus('secondMenuPower', 'NOT_VALIDATED')
                //    .validateField('secondMenuPower');
            }
            else if ($currentInput.attr('name') == 'secondMenuPower') {
                //验证权限
                $('#defaultFormS')
                    .data('bootstrapValidator')
                    .updateStatus('secondMenuPower', 'NOT_VALIDATED')
                    .validateField('secondMenuPower');
            }
            //操作二级菜单
            if ($currentInput.attr('name') == 'secondMenuLevelName') {
                var url = 'handler/GetDataHandler.ashx';
                var requestData = {
                    cmd: 'getMenu',
                    userPower: '0'
                };
                $.get(url, requestData, function (data) {
                    if (data.length > 0) {
                        var dataJson = $.parseJSON(data);
                        var html = '';
                        var htmlOrder = '';
                        $.each(dataJson, function (index, item) {
                            if (item.menu_cn_name == $text) {
                                if (item.child.length > 0) {                                   
                                    $.each(item.child, function (indexChild, itemChild) {
                                        if (indexChild == 0) {
                                            html += '<div class="input-group"><div class="input-group-btn"><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
                                            html += itemChild.menu_cn_name;
                                            html += '<span class="caret"></span></button><ul class="dropdown-menu">';
                                        }
                                        html += '<li><a style="cursor:pointer;" data-menuPower="' + itemChild.menu_power + '" data-menuUrl="' + itemChild.menu_url + '" data-parentID="' + itemChild.menu_parent_id + '" data-menuID="' + itemChild.menu_id + '" freeze="' + itemChild.freeze + '">' + itemChild.menu_cn_name + '</a></li>';
                                        //初始化二级菜单排序控件
                                        htmlOrder += '<li class="ui-state-default" data-parentID="' + itemChild.menu_parent_id + '">' + itemChild.menu_cn_name + '</li>';
                                    });
                                    html += '</ul></div><input type="text" class="form-control" name="secondMenuName" id="secondMenuName" readonly /></div>';
                                    //初始化二级菜单排序控件
                                    $('#sortableS').html(htmlOrder);
                                    if (htmlOrder != '') {
                                        $('#sMenuOrder').hide('slow');
                                        $('#sMenuOrder').show('slow');
                                    } 
                                }
                                else {
                                    html = '<input type="text" class="form-control" name="secondMenuName" id="secondMenuName" readonly />';
                                    $('#sMenuOrder').hide('slow');
                                }
                                $('#second-level-Drop-down').html(html);
                            }                            
                        });                        
                    }                    
                });
            }
        });
        
    });
})(jQuery);
