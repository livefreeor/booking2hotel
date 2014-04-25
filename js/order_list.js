
        $(document).ready(function () {
		$("#dateStart").datepicker();
		$("#dateEnd").datepicker();
		//alert("hello");
            // Check Hash And REdirect
            var hash = location.hash;
            if (hash) {
                hash = hash.replace('#', '');
                var Url = $("window").context.URL.toString();

                var Url_split1 = Url.split("#")[0];
                var Url_split2 = Url_split1.split("?")[0];

                location.href = Url_split2 + hash;

            }

            //SetDefaultBoxSearch();

            var radioSortby = $('input[name="test"]:checked').val()
            var hDest = $('input[name="destDefault"]').val()
            var hLocation = $('input[name="locDefault"]').val()
            var hCategory = $('input[name="category"]').val()



            if (GetQueryStringAll() == null) {
                AjaxgetPageContent(hDest, hLocation, hCategory, 1, radioSortby);
            }
            else {

                var qPage = GetValueQueryString("page");
                var qSort = GetValueQueryString("sort_by");

                if (qPage != "" && qSort != "") {
                    AjaxgetPageContent(hDest, hLocation, hCategory, qPage, qSort);
                    $('input[name="test"]').filter(function (index) { return $(this).attr("value") == qSort }).attr('checked', true);
                }

            }

            $("#sortby_form :radio").each(function () {
                var Val = $(this).attr('value');
                var js = "AjaxGetContentSortBy('" + Val + "');";
                var newclick = new Function(js);
                $(this).attr('onclick', '').click(newclick);
            });


        });


        function SetDefaultBoxSearch() {
            var h_month_day_in = $('input[name="month_day_in"]').val()
            var h_month_year_in = $('input[name="month_year_in"]').val()
            var h_month_day_out = $('input[name="month_day_out"]').val()
            var h_month_year_out = $('input[name="month_year_out"]').val()
            if (h_month_day_in != null && h_month_year_in != null && h_month_day_out != null && h_month_year_out != null) {
                $("#list_boxsearch :text #dateStart").html("TEST");
            }
        }

        function AjaxGetContentSortBy(Sort) {
            var hash = window.location.hash;
            var hashPage = getHashVars()["page"];
            var hashSort = getHashVars()["sort_by"];

            var hDest = $('input[name="destDefault"]').val()
            var hLocation = $('input[name="locDefault"]').val()
            var hCategory = $('input[name="category"]').val()
            var radioSortby = $('input[name="test"]:checked').val()

            var qPage = GetValueQueryString("page");
            var qSort = GetValueQueryString("sort_by");

            if (GetQueryStringAll() == null) {
                
                window.location.hash = "?page=1&sort_by=" + Sort;
                AjaxgetPageContent(hDest, hLocation, hCategory, 1, Sort);
                $('input[name="test"]').filter(function (index) { return $(this).attr("value") == Sort }).attr('checked', true);

            }
            else {
                window.location.hash = "?page=1&sort_by=" + Sort;
                AjaxgetPageContent(hDest, hLocation, hCategory, 1, Sort);

            }


        }

        function AjaxGetContentPage(Page) {

            var hash = window.location.hash;
            
            var hashPage = getHashVars()["page"];
            var hashSort = getHashVars()["sort_by"];

            var hDest = $('input[name="destDefault"]').val()
            var hLocation = $('input[name="locDefault"]').val()
            var hCategory = $('input[name="category"]').val()
            var radioSortby = $('input[name="test"]:checked').val()

            var qPage = GetValueQueryString("page");
            var qSort = GetValueQueryString("sort_by");

            if (GetQueryStringAll() == null) {
                if (hash) {
                    window.location.hash = "?page=" + Page + "&sort_by=" + hashSort;
                    AjaxgetPageContent(hDest, hLocation, hCategory, Page, hashSort);
                } else {
                    window.location.hash = "?page=" + Page + "&sort_by=1";
                    AjaxgetPageContent(hDest, hLocation, hCategory, Page, 1);
                }
            }
            else {

                window.location.hash = "?page=" + Page + "&sort_by=" + qSort;
                AjaxgetPageContent(hDest, hLocation, hCategory, Page, hashSort);
            }


        }

        function AjaxgetPageContent(desId, LocId, CatId, Page, Sortby) {
            $("<img class=\"img_progress\" src=\"../images/progress.gif\" alt=\"Progress\" />").insertBefore("#Product-list").ajaxStart(function () {
                $(this).show();
            }).ajaxStop(function () {
                $(this).remove();
            });


            $.get("subpage.aspx?dest=" + desId + "&loc=" + LocId + "&cate=" + CatId + "&page=" + Page + "&sort_by=" + Sortby, function (data) {
                $("#Product-list").html(data);

                $("#page_navigator a").each(function () {
                    var Aval = $(this).html();
                    var js = "AjaxGetContentPage('" + Aval + "');return false;";
                    var newclick = new Function(js);
                    $(this).attr('onclick', '').click(newclick);
                });
            });

        }
		