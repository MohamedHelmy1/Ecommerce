$(document).ready(function () {
    //card
    //Card();
    //Search


    $("#search").on("keyup", function () {

        GetPageData(1)


    });
    //slider
    GetSliderData();
    ////SeeProduct

});
function AddToCard(id) {

    $.ajax({
        url: "/Home/AddCard?productid=" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result != 1) {
                var html = '';
                var i = 0;

                html += '<li ><a style="color: #fd6883;" href="productcardpage">Go To CardPage</a></li >';
                $.each(result, function (key, item) {
                    i++
                    html += '<li class="divider" ></li >';
                    html += ' <li style="padding: 0px 10px;">' + item.prodect.ProductName + ' <i  onclick=Deletefromcard(' + item.prodect.ProductId + ') style="float: right; margin - right: 15px;color: red;" class="fa fa-times"></i></li>';

                });

                $('#Cartitem').html(html);
                $('#numbercart').html(i);
                toastr.success('Product Added.', '', { timeOut: 5000 })
            }
            else {
                toastr.error('You add this product before!', '', { timeOut: 5000 })
            }

        }

    });
}
function Deletefromcard(id) {

    $.ajax({
        url: "/Home/removeproduct?productid=" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = '';
            var i = 0;
            html += '<li ><a style="color: #fd6883;" href="productcardpage">Go To CardPage</a></li >';

            $.each(result, function (key, item) {
                i++
                html += '<li class="divider" ></li >';
                html += ' <li style="padding: 0px 10px;">' + item.prodect.ProductName + ' <i  onclick=Deletefromcard(' + item.prodect.ProductId + ') style="float: right; margin - right: 15px;color: red;" class="fa fa-times"></i></li>';

            });
            $('#Cartitem').html(html);
            $('#numbercart').html(i);
        }




    });
}
$(document).ready(function () {
    //Initially load pagenumber=1
    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    var search = $("#search").val();
    //After every trigger remove previous data and paging
    $("#paged").empty();
    $.getJSON("/Home/AllProdect?search=" + search, { pageNumber: pageNum, pageSize: pageSize }, function (result) {
        var html = '';
        $.each(result.Data, function (key, item) {






            html += ' <div class="col-md-3 col-sm-6 col-xs-12" style="margin-bottom:8px">';
            html += ' <div class="thumbnail product-item" style="height:300px">';
            html += ' <img class="img-responsive" title="Click to View Product detail" style = "cursor:pointer;height:160px;width:100%" src="/image/product image/' + item.ProductImage + '" />';
            html += '<div class="caption">';
            html += '<h5>' + item.ProductName + '</h5>';
            html += '<p>' + item.price + '$' + '</p>';

            if (item.Quantity == 0) {
                html += '<p> UnAvailable </p>';
            }
            else {
                html += '<button  onclick=AddToCard(' + item.ProductId + ') class="pull-right"><i class="fa fa-shopping-cart"></i></button>';

                html += '<p> Available </p>';
            }
            html += '<div class="product-item-badge">' + item.IsFeature + '</div>';
            html += '</div>';
            html += '</div>';
            html += '</div>';


        });
        $('.tbody').html(html);
        PaggingTemplate(result.TotalPages, result.CurrentPage);
    });
}
        //This is paging temlpate ,you should just copy paste
function GetSliderData() {

    $.ajax({
        url: "/Admin/GetAllSliderData",
        type: "Get",
        contenttype: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


            var html = '';
            var ol = '';
            var i = 0;
            $.each(result, function (key, item) {

                i++;
                if (i == 1) {
                    ol += '<li data-target="#themeSlider" data-slide-to="' + i + '" class="active"></li>';
                    html += '<div class="item active">';
                    html += '<img style="height:320px;width:100%" src="/image/slider image/' + item.SlideImage + '" alt="First slide">';
                    html += ' <div class="carousel-caption">';
                    html += '<h3>' + item.SlideTitle + '</h3>';
                    html += '</div>';
                    html += '</div>';

                }
                else {
                    ol += '<li data-target="#themeSlider" data-slide-to="' + i + '" ></li>';

                    html += '<div class="item">';
                    html += '<img style="height:320px;width:100%" src="/image/slider image/' + item.SlideImage + '" alt="First slide">';
                    html += ' <div class="carousel-caption">';
                    html += '<h3>' + item.SlideTitle + '</h3>';
                    html += '</div>';
                    html += '</div>';
                }

            });
            $('.carousel-inner').html(html);
            $('.carousel-indicators').html(ol);


        }
    });
}