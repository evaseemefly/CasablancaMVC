function CartDetailViewModel(model) {
    var self = this;

    self.sending = ko.observable(false);

    self.cart = model;

    //便利cartItems
    for (var i = 0; i < self.cart.cartItems.length; i++) {
        //将quantity转换为一个可监控属性
        //并拓展使用之前创建的subTtotal拓展
        self.cart.cartitems[i].quantity = ko.observable(self.cart.cartItems[i].quantity)
            .extend({ subTotal: self.cart.cartItems[i].book.salePrice });
    }

    //将carItems设置为可监控数组
    self.cart.cartItems = ko.observableArray(self.cart.cartItems);

    //设置cart的total变量，将购物车总数保存在该变量上
    self.cart.total = self.cart.cartItems.total();

    //设置cartItemBegingChanged变量：在deleteCartItem函数中进行设置
    //在成功删除的时候使用，将其从cartItems数组中删除
    self.cartItemBegingChanged = null;

    self.deleteCartItem = function (cartItem) {
        //显示进度条，并隐藏删除按钮
        self.sending(true);

        //传入的物品设置为要被删除的
        self.cartItemBegingChanged = cartItem;

        $.ajax({
            url: '/api/cartitems',
            type: 'delete',
            contentType: 'application/json',
            data: ko.toJSON(cartItem)
        })
        .success(self.successfulDelete)
        .error(self.errorSave)
        .complete(function () { self.seding(false) });//隐藏进度条
    };

    self.successfulDelete = function (data) {
        //在页面上添加一个成功提示
        $('.body-content').prepend('<div class="alert alert-success"><strong>Success!</strong> The item has been deleted from your cart.</div>');

        //从购物车数组中删除之前设置的cartItemBegingChanged变量
        self.cart.cartItems.remove(self.cartItemBegingChanged);

        //调用了全局的cartSummaryViewModel中的deleteCartItem函数，删除指定的物品
        cartSummaryViewModel.deleteCartItem(ko.toJSON(self.cartItemBegingChanged));

        //成功删除后将该变量设置为空
        self.cartItemBegingChanged = null;
    };

    self.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error updating the item to your cart.</div>');
    };

    //在Carts/Index视图中的ko foreach函数中设置的beforeRemove中调用
    self.fadeOut = function (element) {
        $(element).fadeOut(1000, function () {
            //元素从html中移除
            $(element).remove();
        })
    };
}