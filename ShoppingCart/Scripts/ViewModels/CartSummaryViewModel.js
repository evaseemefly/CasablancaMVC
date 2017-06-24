function CartSummaryViewModel(model) {
    var self = this;

    self.cart = model;

    for (var i = 0; i < self.cart.cartItems.length; i++) {
        var cartItem = self.cart.cartItems[i];
        //?
        cartItem.quantity = ko.observable(cartItem.quantity).extend({ subTotal: cartItem.book.salePrice });


    }

    self.cart.cartItems = ko.observableArray(self.cart.cartItems);

    self.cart.total = self.cart.cartItems.total();

    self.showCart = function () {
        $("#cart").popover("toggle");
    };

    self.fadeIn = function (element) {
        setTimeout(function () {
            $("#cart").popover('show');

            $(element).slideDown(function () {
                setTimeout(function () {
                    $('#cart').popover('hide');
                }, 2000);
            });
        }, 100);
    };

    $("#cart").popover({
        html: true,//向弹出框插入 HTML。如果为 false，jQuery 的 text 方法将被用于向 dom 插入内容。如果您担心 XSS 攻击，请使用 text。
        content: function () {
            return $('#cart-summary').html();

        },
        title: '购物车详情',
        placement: 'bottom',//规定如何定位弹出框（即 top|bottom|left|right|auto）。 当指定为 auto 时，会动态调整弹出框。例如，如果 placement 是 "auto left"，弹出框将会尽可能显示在左边，在情况不允许的情况下它才会显示在右边。
        animation: true,//向弹出框应用 CSS 褪色过渡效果。
        trigger: 'manual'//	定义如何触发弹出框： click| hover | focus | manual。您可以传递多个触发器，每个触发器之间用空格分隔。
    });

};

if (cartSummaryData != undefined) {
    var cartSummaryViewModel = new CartSummaryViewModel(cartSummaryData);

    ko.applyBindings(cartSummaryViewModel, document.getElementById("cart-details"));
} else {
    $('.body-content').prepend('<div class="alert alert-danger"><strong>错误</strong>找不到购物车</div>');
}