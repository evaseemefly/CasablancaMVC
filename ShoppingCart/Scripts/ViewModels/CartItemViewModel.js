function CartItemViewModel(params) {
    var self = this;
    self.sending = ko.observable(false);

    self.cartItem = params.cartItem;

    self.showButton = params.showButton;

    //提交表单时调用的函数
    self.upsertCartItem = function (form) {
        //验证表单是否有效
        if (!$(form).valid())
            return false;
        //设置可监控变量sending为true
        self.sending(true);
        //准备传回给服务器的对象
        var data = {
            id: self.cartItem.id,
            cartId: self.cartItem.book.id,
            bookId:self.cartItem.book.id,
            quantity: self.cartItem.quantity()
        };
        //ajax执行请求
        $.ajax({
            url: '/api/caritems',
            type: self.cartItem.id == undefined ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(data)
        })
        .success(self.successfulSave)   //设置成功、失败及完成时 的回调函数
        .error(self.errorSave)
        .complete(function(){self.sending(false)});

        self.successfulSave = function (data) {

            var msg = '<div class="alert aler-success"><strong>成功！</strong>已经添加';
            if (self.cartItem.id == undefined)
                msg += '添加至';
            else
                msg += '更新至';
            $('.body-content').prepend(msg + '你的购物车.</div>');
            self.cartItem.id = data.id;

            cartSummaryViewModel.updateCartItem(ko.toJSON(self.cartItem));
        }

        self.errorSave = function () {
            var msg = '<div class="alert alert-danger"><strong>错误</strong>有错误';
            if (self.cartItem.id == undefined)
                msg += 'adding';
            else
                msg += '更新';
            $('.body-content').prepend(msg + '这个对象添加至你的购物车.</div>');
        };

    };   
}

ko.components.register('upsert-cart-item', {
    viewModel: CartItemViewModel,
    template: { element: 'cart-item-form' }
});