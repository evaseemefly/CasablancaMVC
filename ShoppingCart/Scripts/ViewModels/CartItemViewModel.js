function CartItemViewModel(params) {
    var self = this;
    self.sending = ko.observable(false);

    self.cartItem = params.cartItem;

    self.showButton = params.showButton;

    self.upsertCartItem = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        var data = {
            id: self.cartItem.id,
            cartId: self.cartItem.book.id,
            quantity: self.cartItem.quantity()
        };

        $.ajax({
            url: '/api/caritems',
            type: self.cartItem.id == undefined ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(data)
        })
        .success(self.successfulSave)
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

    ko.components.register('upsert-cart-item', {
        viewModel: CartItemViewModel,
        template: { element: 'cart-item-form' }
    });
}