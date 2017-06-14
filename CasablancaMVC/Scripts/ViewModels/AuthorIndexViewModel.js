function AuthorIndexViewModel(authors) {

    var self = this;

    self.authors = authors;

    
    //用户单击delete时被调用
    self.showDeleteModal = function (data, event) {
        //有何用处？？
        self.sending = ko.observable(false);

        $.get($(event.target).attr('href'), function (d) {
            $('.body-content').prepend(d);
            $('#deleteModal').modal('show');

            ko.applyBindings(self, document.getElementById('deleteModal'));
        });
    };

    self.deleteAuthor = function (form) {
        self.sending(true);
        return true;
    }
}