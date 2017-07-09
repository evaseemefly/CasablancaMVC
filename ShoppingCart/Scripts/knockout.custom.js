//ko.bindingHandlers.isDirty = {
//    //要绑定的元素、要绑定的属性、该元素上的所有其他绑定、viewModel（已被弃用）、绑定上下文
//    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
//        var originalValue = ko.unwrap(valueAccessor());
//        var interceptor = ko.pureComputed(function () {
//            return (bindingContext.$data.showButton != underfined && bindingContext.$data.showButton) || originalValue != valueAccessor()();
//        });

//        ko.applyBindingsToNode(element, {
//            visible: interceptor
//        });
//    }
//}
ko.bindingHandlers.isDirty = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //ko.unwrap=unwrapObservable
        //首先将quantity的值保存到一个变量中
        var originalValue = ko.unwrap(valueAccessor());

        //决定提交按钮的显示和隐藏状态
        var interceptor = ko.pureComputed(function () {
            return (bindingContext.$data.showButton !== undefined && bindingContext.$data.showButton)
                    || originalValue != valueAccessor()();
        });

        //拓展了visible绑定
        ko.applyBindingsToNode(element, {
            visible: interceptor
        });
    }
};


ko.extenders.subTotal = function (target, mulptiplier) {
    target.subTotal = ko.observable();

    function calculateTotal(newValue) {
        target.subTotal((newValue * multiplier).toFixed(2));
    }

    calculateTotal(target());

    target.subscribe(calculateTotal);
    return target;
}

ko.observableArray.fn.total = function () {
    return ko.pureComputed(function () {
        var runningTotal = 0;

        for (var i = 0; i < this().length; i++) {
            runningTotal += parseFloat(this()[i].quantity.subTotal());
        }

        return runningTotal.toFixed(2);
    }, this);
}