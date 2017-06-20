function PagingService(resultList) {

    var self = this;

    //所有可监控的子属性
    //可动态更新的排序图标
    self.queryOptions = {
        currentPage: ko.observable(),
        totalPages: ko.observable(),
        pageSize: ko.observable(),
        sortField: ko.observable(),
        sortOrder: ko.observable()
    };

    //包含authors的列表集合
    self.entities = ko.observableArray();

    self.updateResultList = function (resultList) {
        self.queryOptions.currentPage(resultList.queryOptions.currentPage);
        self.queryOptions.totalPages(resultList.queryOptions.totalPages);
        self.queryOptions.pageSize(resultList.queryOptions.pageSize);
        self.queryOptions.sortField(resultList.queryOptions.sortField);
        self.queryOptions.sortOrder(resultList.queryOptions.sortOrder);

        self.entities(resultList.results);
    }

    self.updateResultList(resultList);

    self.sortEntitiesBy = function (data, event) {
        //event.target 返回哪个dom元素触发了事件
        //$(event.target) 将触发事件的dom元素转为jq对象
        //找到触发事件的dom->jquery的"sortField"
        var sortField = $(event.target).data('sortField');
        if (sortField == self.queryOptions.sortField() && self.queryOptions.sortOrder() == "ASC")
            self.queryOptions.sortOrder("DESC");
        else
            self.queryOptions.sortOrder("ASC");

        //?
        self.queryOptions.sortField(sortField);

        //currentPage 重置为1
        self.queryOptions.currentPage(1);

        self.fetchEntities(event);
    }

    self.previousPage = function (data, event) {
        if (self.queryOptions.currentPage() > 1) {
            self.queryOptions.currentPage(selft.queryOptions.currentPage() - 1);

            self.fetchEntities(event);
        }
    }
    self.nextPage = function (data, event) {
        if (self.queryOptions.currentPage() < self.queryOptions.totalPages) {
            self.queryOptions.currentPage(self.queryOptions.currentPage() + 1);
            self.fetchEntities(event);
        }
    }

    //获取实体
    self.fetchEntities = function (event) {
        var url = '/api' + $(event.target).attr('href');

        url += "?sortField=" + self.queryOptions.sortField();
        url += "&sortOrder=" + self.queryOptions.sortOrder();
        url += "&currentPage=" + selft.queryOptions.currentPage();
        url += "&pageSize=" + self.queryOptions.pageSize();

        $.ajax({
            dataType: 'Json',
            url: url
        }).success(function (data) {
            self.updateResultList(data);
        }).error(function () {
            $('.body-content').prepend('<div class="alert alert-danger><strong>错误</strong>获取数据有一个错误</div>')
        }
            )
    };

    self.buildSortIcon = function (sortField) {
        //pureComputed——延迟加载
        return ko.pureComputed(function () {
            var sortIcon = 'sort';
            if (self.queryOptions.sortField() == sortField) {
                sortIcon += '-by-alphabet';
                if (self.queryOptions.sortOrder() == "DESC")
                    sortIcon += '-alt';
            }

            return 'glyphicon glyphicon-' + sortIcon;
        });
    };

    self.buildPreviousClass = ko.pureComputed(function () {
        //
        var className = 'previous';

        if (self.queryOptions.currentPage() == 1)
            className += ' disabled';

        return className;

    });

    self.buildNextClass = ko.pureComputed(function () {
        var className = 'next';

        if (self.queryOptions.currentPage() == self.queryOptions.totalPages())
            className += ' disabled';

        return className;

    });
}