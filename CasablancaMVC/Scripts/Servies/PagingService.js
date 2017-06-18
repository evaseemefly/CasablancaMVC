function PagingService(resultList) {

    var sefl = this;

    self.queryOptions = {
        currentPage: ko.observable(),
        totalPage: ko.observable(),
        pageSize: ko.observable(),
        sortField: ko.observable(),
        sortOrder: ko.observable()
    };

    self.entities = ko.observableArray();

    self.updateResultList = function (resultList) {
        self.queryOptions.currentPage(resultList.queryOptions.currentPage);
        self.queryOptions.totalPage(resultList.queryOptions.totalPage);
        self.queryOptions.pageSize(resultList.queryOptions.pageSize);
        self.queryOptions.sortField(resultList.queryOptions.sortField);
        self.queryOptions.sortOrder(resultList.queryOptions.sortOrder);

        self.entities(resultList.results);
    }

    self.updateResultList(resultList);
}