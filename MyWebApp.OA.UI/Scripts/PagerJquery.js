jQuery.Pager = function(pageIndex, pageSize, totalCount, url) {
    if (totalCount < 1) {
        return;
    }
    //总页数
    var totalPages = Math.max((totalCount + pageSize - 1) / pageSize, 1);
    var size = pageSize == 0 ? 3 : pageSize;
    //定义一个文档碎片
    var docFragment = $(document.createDocumentFragment());
    //处理当前页等于1的时候
    if (pageIndex != 1) {
        docFragment.append($("<a  class='pageLink' href='" + url + "&pageIndex=1&pageSize=" + pageSize + "'>首页</a>"));
    }
    //处理上一页链接
    if (pageIndex > 1) {
        docFragment.append($("<a class='pageLink' href='url" + "&pageIndex=" + (pageIndex - 1) + "&pageSize=" + pageSize + "'>上一页</a>"));

    }
    var currint = 5;
    for (var i = 0; i <= 10; i++) {//一共最多显示10个页码，前面5个，后面5个
        if ((pageIndex + i - currint) >= 1 && (pageIndex + i - currint) <= totalPages) {
            if (currint == i) {//当前页处理
                docFragment.append($("<a class='cpb' href='" + url + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "'>" + pageIndex + "</a> "));

            }
            else {//一般页处理

                docFragment.append($("<a  class='pageLink' href='" + url + "&pageIndex=" + (pageIndex + i - currint) + "&pageSize=" + pageSize + "'>" + (pageIndex + i - currint) + "</a> "));

            }

        }
    }

    //处理下一页的链接
    if (pageIndex <=totalPages) {
        docFragment.append($("<a class='pageLink' href='" + url + "&pageIndex=" + (pageIndex + 1) + "&pageSize=" + pageSize + "'>下一页</a> "));
    }
    if (pageIndex != totalPages) {
        docFragment.append($("<a class='pageLink' href='" + url + "&pageIndex=" + (pageIndex + 1) + "&pageSize=" + pageSize + "'>末页</a> " + "第" + pageIndex + "页 / 共" + totalPages + "页"));
    }

    return docFragment;
}
