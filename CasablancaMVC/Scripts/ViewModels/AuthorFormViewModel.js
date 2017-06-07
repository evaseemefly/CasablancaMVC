﻿function AuthorFormViewModel() {
    var self = this;

    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

    self.author = {
        firstName: ko.observable(),
        lastName: ko.observable(),
        biography: ko.observable()
    };

    self.validateAndSave = function (form) {
        //进行验证
        //若没通过jquery验证，则不会提交表单
        if (!$(form).valid())
            return false;

        self.sending(true);

        self.author._RequestVerificationToken = form[0].value;

        //绑定提交
        $.ajax({
            url: 'Create',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(self.author)
        })
        .success(self.successfulSave)   //成功后的回调函数
        .error(self.errorSave)          //请求失败时的回调函数
        .complete(function () { self.sending(false) });//请求完成后的回调函数
    };

    self.successfulSave = function () {
        self.saveCompleted(true); 

        $('.body-content').prepend(
            '<div class="alert alert-success"><strong>Success!</strong>已经存储</div>');
        //向用户显示成功提示消息后
        //停止1秒，并重新跳转至authors列表页面
        setTimeout(function () {
            location.href = './';
        }, 1000);
    };

    self.errorSave = function () {
        $('.body-content').prepend(
            '<div class="alert alert-danger"><strong>Error!</strong>已经存储</div>');
    };
}