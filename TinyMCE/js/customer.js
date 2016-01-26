/**
 * Created by XiaoDong on 2015/11/25.
 */
//加载页面
(function($){
    $(document).ready(function(){

        //tab
        $(function() {
            $( "#tabs" ).tabs({
                event: "mouseover"
            });
        });
        ////初始化tooltip
        $(function () { $("[data-toggle='tooltip']").tooltip(); });
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

        //加载旋转木马
        if($('#carousel').length > 0){
            $('#carousel').load('index-carousel.aspx #carousel-example-generic');
        }
        //加载页头和页脚
        if ($('#header').length > 0) {
            $('#header').load('header-footer.aspx .header');
        }
        if ($('#footer').length > 0) {
            $('#footer').load('header-footer.aspx .footer');
        }
        //加载首页右侧标签
        if ($('#right-tab').length > 0) {
            $('#right-tab').load('index-tab.aspx #index-tabs');
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
                    var firstHtml = html + '</ul></div><input type="text"class="form-control"name="firstMenuName" id="firstMenuName"/></div>';
                    var secondHtml = html + '</ul></div><input type="text"class="form-control"name="secondMenuLevelName" readonly/></div>';
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
        //启动旋转木马
        $('.carousel').carousel();
        //点击登录、注册
        if ($('.header-login').length > 0) {
            $('.header-login').click(function (event) {
                var showModal = false;
                var currentHeaderClick = '';
                if (event.target.id == 'login') {
                    $.ajaxSetup({
                        async: false
                    });
                    $('#login-register').load('login.aspx #exampleModal-login');
                    showModal = true;
                    currentHeaderClick = 'login';
                } else if (event.target.id == 'register') {
                    $.ajaxSetup({
                        async: false
                    });
                    $('#login-register').load('register.aspx #exampleModal-register');
                    showModal = true;
                    currentHeaderClick = 'register'
                }
                if (showModal) {
                    $.getScript('js/bootstrapValidator.js');
                    $.getScript('js/bootstrapValidatorRemote.js');
                    //               $.getScript('css/bootstrap.css');
                    $.getScript('css/bootstrapValidator.css');
                    if (currentHeaderClick == 'login') {
                        $('#exampleModal-login').modal();
                    } else if (currentHeaderClick == 'register') {
                        $('#exampleModal-register').modal();
                    }
                }
            });
        }
        //获取详情列表
        if ($('.pager').length > 0) {
            $.getJSON('data/detail.json', function (data) {
                $.each(data, function (index, item) {
                    $('.pager').detaillist(item);
                });
            });
        }
        //论坛页面
        $('.lt-title-right a').click(function () {
            var $this = $(this);
            var $img = $(this).find('img');
            if ($img.hasClass('collapsed_no')) {
                $img
                    .attr({
                        src: 'images/collapsed_yes.gif.png'
                    })
                    .removeClass()
                    .addClass('collapsed_yes');
                $this.closest('.lt-container-class').find('.lt-content').hide();
            } else {
                $img
                    .attr({
                        src: 'images/collapsed_no.gif.png'
                    })
                    .removeClass()
                    .addClass('collapsed_no');
                $this.closest('.lt-container-class').find('.lt-content').show();
            }

        });
        $('.lt-container-class').each(function (index, item) {
            var $item = $(item);
            var $content = $(item).find('.lt-content');
            if ($content.length > 1) {
                $content.each(function (index, item1) {
                    if (index == ($content.length - 1)) {
                        return false;
                    }
                    $(item1).css({
                        'border-bottom': '1px dashed #CDCDCD'
                    });
                })
            }
        });
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
                $('#defaultFormS')
                    .data('bootstrapValidator')
                    .updateStatus('secondMenuName', 'NOT_VALIDATED')
                    .validateField('secondMenuName');
                //权限赋值
                $('#secondMenuPower').val($this.attr('data-menuPower'));
                //url赋值
                $('#secondMenuNameUrl').val($this.attr('data-menuUrl'));
                //是否冻结赋值
                if ($this.attr('freeze') == '1') {
                    $('#freezeSecondMenu').prop('checked', 'true');
                } else {
                    $('#freezeSecondMenu').removeAttr('checked');
                }
                //验证权限
                $('#defaultFormS')
                    .data('bootstrapValidator')
                    .updateStatus('secondMenuPower', 'NOT_VALIDATED')
                    .validateField('secondMenuPower');
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
                                    html += '</ul></div><input type="text" class="form-control" name="secondMenuName" id="secondMenuName"/></div>';
                                    //初始化二级菜单排序控件
                                    $('#sortableS').html(htmlOrder);
                                    if (htmlOrder != '') {
                                        $('#sMenuOrder').hide('slow');
                                        $('#sMenuOrder').show('slow');
                                    } 
                                }
                                else {
                                    html = '<input type="text" class="form-control" name="secondMenuName" id="secondMenuName" />';
                                    $('#sMenuOrder').hide('slow');
                                }
                                $('#second-level-Drop-down').html(html);
                                $('#defaultFormS').bootstrapValidator({
                                    //        live: 'disabled',
                                    message: 'This value is not valid',
                                    feedbackIcons: {
                                        valid: 'glyphicon glyphicon-ok',
                                        invalid: 'glyphicon glyphicon-remove',
                                        validating: 'glyphicon glyphicon-refresh'
                                    },
                                    fields: {
                                        secondMenuPower: {
                                            validators: {
                                                notEmpty: {
                                                    message: '用户权限不能为空'
                                                },
                                                regexp: {
                                                    regexp: /^游客$|^管理员$/,
                                                    message: '权限值只能是游客或管理员'
                                                }
                                            }
                                        },
                                        secondMenuLevelName: {
                                            validators: {
                                                notEmpty: {
                                                    message: '一级级菜单不能为空'
                                                }
                                            }
                                        },
                                        secondMenuName: {
                                            validators: {
                                                notEmpty: {
                                                    message: '二级级菜单不能为空'
                                                }
                                            }
                                        },
                                        secondMenuNameUrl: {
                                            validators: {
                                            }
                                        }
                                    }
                                }).on('success.form.bv', secondSubmitFun);
                            }                            
                        });                        
                    }                    
                });
            }
        });
        //一级菜单排序提交
        $('#saveOrderF,#saveOrderS').click(function (event) {
            var menuNameList = '';
            var parentID = '';
            var targetID = event.target.id;
            //$('#' + targetID + ' li').each(function (index, item) {
            //$('#' + targetID).closest('.row').find('.sortable li').each(function (index, item) {
            //    menuNameList += $(item).text() + '&';
            //    parentID = $(item).attr('data-parentID');
            //});
            if (targetID == 'saveOrderF') {
                $('#sortableF li').each(function (index, item) {
                    menuNameList += $(item).text() + '&';
                    parentID = $(item).attr('data-parentID');
                });
            } else if (targetID == 'saveOrderS') {
                $('#sortableS li').each(function (index, item) {
                    menuNameList += $(item).text() + '&';
                    parentID = $(item).attr('data-parentID');
                });
            }
            menuNameList = menuNameList.substr(0, menuNameList.length - 1);
            var requestData = {
                cmd: 'menuOrder',
                menuNameList: encodeURI(menuNameList),
                parentID: parentID
            };
            var url = 'handler/GetDataHandler.ashx';
            $.post(url, requestData, function (data) {
                if (data == '1') {
                    $('#' + targetID + 'Success').show('slow');
                    setTimeout('hideMessageSuccess()', 3000);
                } else {
                    $('#' + targetID + 'Fail').show('slow');
                    setTimeout('hideMessageFail()', 3000);
                }
            });
        });
        //删除一级菜单
        $('#deleteFMenu,#deleteSMenu').click(function (event) {
            var id = event.target.id;
            var menuName = ''
            var text = '';
            var parentID = '';
            var menuID = ''
            if (id == 'deleteFMenu') {
                menuName = $('#firstMenuName').val();
                //parentID = $('#firstMenuName').attr('data-parentID');
                menuID = $('#firstMenuName').attr('data-menuID');
                text = '您确定要删除一级菜单【' + menuName + '】吗，删除该菜单后，子菜单也会一并删除。'
            } else if (id == 'deleteSMenu') {
                menuName = $('#secondMenuName').val();
                //parentID = $('#secondMenuName').attr('data-parentID');
                menuID = $('#secondMenuName').attr('data-menuID');
                text = '您确定要删除二级菜单【' + menuName + '】吗。'
            }
            if (menuID != null && menuID != '') {
                $('#warningModal').find('.container-fluid').text(text);
                $('#deleteMenuTrue').attr({
                    //menuName: menuName,
                    //parentID: parentID,
                    menuID: menuID
                });
                $('#warningModal').modal('show');
            }
        });
        //确定删除菜单
        $('#deleteMenuTrue').click(function (event) {
            var $this = $(this);
            var requestData = {
                cmd: 'menuDelete',
                //menuName: encodeURI($this.attr('menuName')),
                //parentID: $this.attr('parentID'),
                menuID: $this.attr('menuID')
            };
            var url = 'handler/GetDataHandler.ashx';
            $.post(url, requestData, function (data) {
                if (data == '1') {
                    $('#deleteMenuSuccess').show('slow');
                    setTimeout('hideMessageSuccess();', 3000);
                } else {
                    $('#deleteMenuFail').show('slow');
                    setTimeout('hideMessageFail()', 3000);
                }
            });
        });
    });
})(jQuery);
//一级菜单提交时触发方法
var firstSubmitFun = function (event) {
    event.preventDefault();
    var url = 'handler/GetDataHandler.ashx';
    var $this = $(this);
    var form = $this.serialize();
    var requestData = {
        cmd: 'firstMenuSave',
        form: form
    };
    $.post(url, requestData, function (result) {
        if (result == '1') {
            $('#firstMenuSuccess').show('slow');
            setTimeout('hideMessageSuccess()', 3000);
        } else {
            $('#firstMenuFail').show('slow');
            setTimeout('hideMessageFail()', 3000);
        }
        $this.find('[type=submit]').removeAttr('disabled');
        // ... process the result ...
    });
};
//二级菜单提交时触发方法
var secondSubmitFun = function (event) {
    event.preventDefault();
    var url = 'handler/GetDataHandler.ashx';
    var $this = $(this);
    var form = $this.serialize();
    var requestData = {
        cmd: 'secondMenuSave',
        form: form
    };
    $.post(url, requestData, function (result) {
        if (result == '1') {
            $('#secondMenuSuccess').show('slow');
            setTimeout('hideMessageSuccess()', 3000);
        } else {
            $('#secondMenuFail').show('slow');
            setTimeout('hideMessageFail()', 3000);
        }
        $this.find('[type=submit]').removeAttr('disabled');
        // ... process the result ...
    });
};