
$(document).ready(function () {


    if ($(".headtitle").length) {
        $(".headtitle").filter(function (index) { return index == 0 }).css("display","none") ;
    }
    var hash = location.hash;

    if (hash) {
        hash = hash.replace('#', '');
        var Url = $("window").context.URL.toString();
        var Url_split1 = Url.split("#")[0];
        var Url_split2 = Url_split1.split("?")[0];

        location.href = Url_split2 + hash;
    }
});

$(document).ready(function () {
    var PageSplit = GetValueQueryString("psplit");
    var PageTarget = GetValueQueryString("ptarget");
    var ReviewProduct = GetValueQueryString("reviews");
    var ReviewType = GetValueQueryString("revType");


    $("div[id$='review_result']").html("<img class=\"img_progress\" src=\"../../images/progress_b.gif\" alt=\"Progress\" />");
    $("div[id$='Review_page_title']").html("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />");
    $("div[id$='navigator_top']").html("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />");
    $("div[id$='navigator_bottom']").html("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />");


    if (PageSplit == "" && PageTarget == "" && ReviewProduct == "" && ReviewType == "") {
        //review List
        $.get("ajax_review_post.aspx?reviews=hotels&revType=waiting&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='review_result']").html(data);
        });

        //title page
        $.get("ajax_review_post_page_title.aspx?reviews=hotels&revType=waiting&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $
            $("div[id$='Review_page_title']").html(data);
        });

        //navigator
        $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=waiting&ptarget=1&psplit=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

            $("div[id$='navigator_top']").html(data);
            $("div[id$='navigator_bottom']").html(data);
        });

    }

    if (PageTarget != "") {

        $('#Page_nav_pageList a').filter(function (index) {
            $(this).hover(function () {
                alert(index);
            });
        });
    }
    else {
        $('.Page_nav_pageList a').filter(function (index) {
            return index == 0;
        }).addClass("Page_nav_pageList_activePage");
    }


});



function getReviewList(ReviewProduct, ReviewType, PageTarget, Pagesplit) {
    //review List
    $.get("ajax_review_post.aspx?reviews=" + ReviewProduct + "&revType=" + ReviewType + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='review_result']").html(data);
    });

    //title page
    $.get("ajax_review_post_page_title.aspx?reviews=" + ReviewProduct + "&revType=" + ReviewType + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='Review_page_title']").html(data);
    });

    //navigator
    $.get("ajax_review_post_navigator.aspx?reviews=" + ReviewProduct + "&revType=" + ReviewType + "&ptarget=" + PageTarget + "&psplit=" + Pagesplit + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='navigator_top']").html(data);
        $("div[id$='navigator_bottom']").html(data);
    });

}



function getReviewListAjax(ReviewProduct, linkId) {
    window.location.hash = "?reviews=" + ReviewProduct;
    ReviewActiveLink(linkId);

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("#" + linkId).ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })
    //review List
    $.get("ajax_review_post.aspx?reviews=" + ReviewProduct + "&revType=waiting&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='review_result']").html(data);
    });
    //title page
    $.get("ajax_review_post_page_title.aspx?reviews=" + ReviewProduct + "&revType=waiting&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='Review_page_title']").html(data);
    });
    //navigator
    $.get("ajax_review_post_navigator.aspx?reviews=" + ReviewProduct + "&revType=waiting&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
        $("div[id$='navigator_top']").html(data);
        $("div[id$='navigator_bottom']").html(data);
    });

    ReviewActiveLink_Type("h_waiting");
}

function getReviewListAjaxNavigator(PageSplite, PageTarget, linkId) {

    var hash = window.location.hash;
    var REviewProduct = getHashVars()["reviews"];


    var qReviewProduct = GetValueQueryString("reviews");
    var qReviewType = GetValueQueryString("revType");
    var qReviewsplit = GetValueQueryString("psplit");
    var qReviewTarget = GetValueQueryString("ptarget");


    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("div[id$='Review_page_title']").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    });



    if (!hash) {
        if (qReviewProduct == "" && qReviewType == "" && qReviewsplit == "" && qReviewTarget == "") {
            window.location.hash = "?psplit=" + PageSplite + "&ptarget=" + PageTarget;
            //review List
            $.get("ajax_review_post.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });
        }
        else {
            if (qReviewProduct == "" && qReviewType == "" && qReviewsplit != "" && qReviewTarget != "") {
                window.location.hash = "?psplit=" + PageSplite + "&ptarget=" + PageTarget;
                //review List
                $.get("ajax_review_post.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    $("div[id$='review_result']").html(data);
                });


                //navigator
                $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    $("div[id$='navigator_top']").html(data);
                    $("div[id$='navigator_bottom']").html(data);
                });
            }

            if (qReviewProduct == "" && qReviewType != "" && qReviewsplit == "" && qReviewTarget == "") {
                window.location.hash = "?revType=" + qReviewType + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;
                //review List
                $.get("ajax_review_post.aspx?reviews=hotels&revType=" + qReviewType + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    $("div[id$='review_result']").html(data);
                });


                //navigator
                $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=" + qReviewType + "&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    $("div[id$='navigator_top']").html(data);
                    $("div[id$='navigator_bottom']").html(data);
                });
            }

            if (qReviewProduct != "" && qReviewType == "" && qReviewsplit == "" && qReviewTarget == "") {
                window.location.hash = "?reviews=" + qReviewProduct + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;
                //review List
                $.get("ajax_review_post.aspx?reviews=" + qReviewProduct + "&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    $("div[id$='review_result']").html(data);
                });


                //navigator
                $.get("ajax_review_post_navigator.aspx?reviews=" + qReviewProduct + "&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    $("div[id$='navigator_top']").html(data);
                    $("div[id$='navigator_bottom']").html(data);
                });
            }

            if (qReviewProduct != "" && qReviewType != "" && qReviewsplit == "" && qReviewTarget == "") {
                window.location.hash = "?reviews=" + qReviewProduct + "&revType=" + qReviewType + "psplit=" + PageSplite + "&ptarget=" + PageTarget;
                //review List
                $.get("ajax_review_post.aspx?reviews=" + qReviewProduct + "&revType=" + qReviewType + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    $("div[id$='review_result']").html(data);
                });


                //navigator
                $.get("ajax_review_post_navigator.aspx?reviews=" + qReviewProduct + "&revType=" + qReviewType + "&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    $("div[id$='navigator_top']").html(data);
                    $("div[id$='navigator_bottom']").html(data);
                });
            }

            if (qReviewProduct != "" && qReviewType == "" && qReviewsplit != "" && qReviewTarget != "") {
                window.location.hash = "?reviews=" + qReviewProduct + "&revType=waiting&psplit=" + PageSplite + "&ptarget=" + PageTarget;
                //review List
                $.get("ajax_review_post.aspx?reviews=" + qReviewProduct + "&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                    $("div[id$='review_result']").html(data);
                });


                //navigator
                $.get("ajax_review_post_navigator.aspx?reviews=" + qReviewProduct + "&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                    $("div[id$='navigator_top']").html(data);
                    $("div[id$='navigator_bottom']").html(data);
                });
            }

        }
    }
    else {

        if (getHashVars()["psplit"] != null && getHashVars()["ptarget"] != null && getHashVars()["reviews"] == null && getHashVars()["reviews"] == null && getHashVars()["revType"] == null) {

            window.location.hash = "?psplit=" + PageSplite + "&ptarget=" + PageTarget;

            $.get("ajax_review_post.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        }

        if (getHashVars()["reviews"] != null && getHashVars()["revType"] == null && getHashVars()["psplit"] == null && getHashVars()["ptarget"] == null) {

            window.location.hash = "?reviews=" + getHashVars()["reviews"] + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;

            $.get("ajax_review_post.aspx?reviews=" + getHashVars()["reviews"] + "&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=" + getHashVars()["reviews"] + "&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        }

        if (getHashVars()["reviews"] != null && getHashVars()["revType"] == null && getHashVars()["psplit"] != null && getHashVars()["ptarget"] != null) {

            window.location.hash = "?reviews=" + getHashVars()["reviews"] + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;

            $.get("ajax_review_post.aspx?reviews=" + getHashVars()["reviews"] + "&revType=waiting&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=" + getHashVars()["reviews"] + "&revType=waiting&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        }

        if (getHashVars()["reviews"] != null && getHashVars()["revType"] != null && getHashVars()["psplit"] == null && getHashVars()["ptarget"] == null) {

            window.location.hash = "?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;

            $.get("ajax_review_post.aspx?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        }

        if (getHashVars()["reviews"] != null && getHashVars()["revType"] != null && getHashVars()["psplit"] != null && getHashVars()["ptarget"] != null) {

            window.location.hash = "?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&psplit=" + PageSplite + "&ptarget=" + PageTarget;

            $.get("ajax_review_post.aspx?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&ptarget=" + PageTarget + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });


            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=" + getHashVars()["reviews"] + "&revType=" + getHashVars()["revType"] + "&ptarget=" + PageTarget + "&psplit=" + PageSplite + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {

                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        }
    }

}

function getReviewListAjaxType(ReviewType, linkId) {
    var hash = window.location.hash;
    var REviewProduct = getHashVars()["reviews"];

    var qReviewProduct = GetValueQueryString("reviews");
    var qReviewType = GetValueQueryString("revType");
    ReviewActiveLink_Type(linkId);

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("#" + linkId).ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })


    if (getHashVars()["reviews"] == null) {
        if (qReviewProduct == "") {
            window.location.hash = "?reviews=hotels&revType=" + ReviewType + GetQuerystringProductAndSupplierForBluehouseManage("append");
            //review List
            $.get("ajax_review_post.aspx?reviews=hotels&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });
            //title page
            $.get("ajax_review_post_page_title.aspx?reviews=hotels&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='Review_page_title']").html(data);
            });
            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=hotels&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });

        } else {
            window.location.hash = "?reviews=" + qReviewProduct + "&revType=" + ReviewType;
            //review List
            $.get("ajax_review_post.aspx?reviews=" + qReviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='review_result']").html(data);
            });
            //title page
            $.get("ajax_review_post_page_title.aspx?reviews=" + qReviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='Review_page_title']").html(data);
            });
            //navigator
            $.get("ajax_review_post_navigator.aspx?reviews=" + qReviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
                $("div[id$='navigator_top']").html(data);
                $("div[id$='navigator_bottom']").html(data);
            });
        }


    } else {


        window.location.hash = "?reviews=" + getHashVars()["reviews"] + "&revType=" + ReviewType + GetQuerystringProductAndSupplierForBluehouseManage("append");

        //review List
        $.get("ajax_review_post.aspx?reviews=" + REviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='review_result']").html(data);
        });
        //title page
        $.get("ajax_review_post_page_title.aspx?reviews=" + REviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='Review_page_title']").html(data);
        });
        //navigator
        $.get("ajax_review_post_navigator.aspx?reviews=" + REviewProduct + "&revType=" + ReviewType + "&ptarget=1" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='navigator_top']").html(data);
            $("div[id$='navigator_bottom']").html(data);
        });
    }



}

function getProduductType(strType) {
    var Type = 0;
    switch (strType) {
        case "hotels":
            Type = 29;
            break;
        case "spa":
            Type = 40;
            break;
        case "golfs":
            Type = 32;
            break;
        case "daytrips":
            Type = 34;
            break;
        case "water":
            Type = 36;
            break;
        case "show":
            Type = 38;
            break;
        case "health":
            Type = 39;
            break;
    }
    return Type;
}

function getReviewType(Strtype) {
    var Type = 0;
    switch (Strtype) {
        case "approve":
            Type = 0;
            break;
        case "waiting":
            Type = 1;
            break;
        case "bin":
            Type = 2;
            break;
    }

    return Type;
}
function getReviewDetail(reviewId) {
    var ReviewProduct = GetValueQueryString("reviews");

    
    $("<img style=\"margin:15px 0px 0px 0px; \" class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("div[id$='review_item_" + reviewId + "']").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })

    if (ReviewProduct == "") {
        EnableDiv("review_item_" + reviewId);

        $.get("ajax_review_post_detail.aspx?revId=" + reviewId + "&reviews=hotels" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
           
            $("div[id$='review_item_" + reviewId + "']").html(data);

        });
    }
    else {
        EnableDiv("review_item_" + reviewId);

        $.get("ajax_review_post_detail.aspx?revId=" + reviewId + "&reviews=" + ReviewProduct + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            
            $("div[id$='review_item_" + reviewId + "']").html(data);

        });
    }
}

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

function RestoreTempReviewBlock(reviewId) {
    var temp = $('div.review_temp').html();
    //var temp = $("div[id$='review_item_block_" + reviewId + "']").html();
    var tempEncode = htmlDecode(temp);
    $("div[id$='review_item_block_" + reviewId + "']").html(tempEncode);

    $('html, body').animate({ scrollTop: $("#review_item_block_" + reviewId).offset().top - 100 }, 500);

}

function RevieweditSave(reviewId) {

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("div[id$='review_item_block_" + reviewId + "']").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })
    var ReviewProduct = GetValueQueryString("reviews");
    if (ReviewProduct == "") {
        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION,#__EVENTTARGET,#__EVENTARGUMENT").serialize();

        $.post("ajax_review_edit_save.aspx?revId=" + reviewId + "&reviews=hotels" + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
            $("div[id$='review_item_block_" + reviewId + "']").html(data);
            $('html, body').animate({ scrollTop: $("#review_item_block_" + reviewId).offset().top - 100 }, 500);
        });

    } else {

        var post = $("#form1").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION,#__EVENTTARGET,#__EVENTARGUMENT").serialize();

        $.post("ajax_review_edit_save.aspx?revId=" + reviewId + "&reviews=" + ReviewProduct + GetQuerystringProductAndSupplierForBluehouseManage("append"), post, function (data) {
            $("div[id$='review_item_block_" + reviewId + "']").html(data);
            $('html, body').animate({ scrollTop: $("#review_item_block_" + reviewId).offset().top - 100 }, 500);
        });
    }

}
function StarValSelect() {
    $("#ratebox ul").filter(function (m_index) {
        var ul_id = $(this).attr("id");
        $(this).children("li").filter(function (index) {
            $(this).click(function () {
                var value = index + 1;

                $("#" + ul_id).children("li").unbind("mouseout");

                if ($("#value_hidden_" + ul_id)) {
                    $("#value_hidden_" + ul_id).remove();
                }
                $("#ratebox").after('<input id="value_hidden_' + ul_id + '" type="hidden" name="' + ul_id + '" value="' + value + '" />');

                $("#" + ul_id).children("li").filter(function () {
                    if (index > value - 1) {
                        $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0" });
                        //$(this).css({ "background-image": "Url(../../../images/rate_click.png)" });
                    }
                });

                var InputValue = $('input[name="' + ul_id + '"]').val();
                //alert(InputValue);
                $("#" + ul_id).mouseout(function () {
                    $(this).children("li").filter(function (indexs) {
                        if (indexs < InputValue) {

                            $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0" });
                            //$(this).css({ "background-image": "Url(../../../images/rate_click.png)" });
                        }
                        if (indexs > InputValue - 1) {
                            $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -20px" });
                            //$(this).css({ "background-image": "Url(../../../images/rate_hover.png)" });
                        }
                    });
                });

            });


            $(this).hover(function () {
                //$(this).css({ "background-image": "Url(../../../images/rate_click.png)", "cursor": "pointer" });

                $("#" + ul_id).children("li").filter(function (index2) {
                    if (index >= index2) {
                        $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0", "cursor": "pointer" });
                        //$(this).css({ "background-image": "Url(../../../images/rate_click.png)", "cursor": "pointer" });
                    }
                });

                $("#" + ul_id).children("li").filter(function (index2) {
                    if (index2 > index) {
                        $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -20px", "cursor": "pointer" });
                        //$(this).css({ "background-image": "Url(../../../images/rate_hover.png)", "cursor": "pointer" });
                    }
                });
            });
            var InputValue = $('input[name="' + ul_id + '"]').val();
            $(this).mouseout(function () {
                $("#" + ul_id).children("li").filter(function (indexs) {
                    if (indexs < InputValue) {

                        $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0" });
                        //$(this).css({ "background-image": "Url(../images/rate_click.png)" });
                    }
                    if (indexs > InputValue - 1) {
                        $(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -20px" });
                        //$(this).css({ "background-image": "Url(../images/rate_hover.png)" });
                    }
                    //$(this).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -40px", "cursor": "pointer" });
                    //$(this).css({ "background-image": "Url(../images/rate_default.png)", "cursor": "pointer" });
                });


            });
        });


    });
}

function StarValue(ulId, val) {
   $("#ratebox").after('<input id="value_hidden_' + ulId + '" type="hidden" name="' + ulId + '" value="' + val + '" />');
    if (val > 0) {
        $("#" + ulId).children("li").filter(function (index) {
            return index <= val - 1;
        }).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 0", "cursor": "pointer" });

        $("#" + ulId).children("li").filter(function (index) {
            return index > val - 1;
        }).css({ "background-image": "Url(../../images/write_blank.png)", "background-position": "0 -20px", "cursor": "pointer" });

        StarValSelect();
    }
    
}
function getReviewEdit(reviewId) {
    var temp = $("div[id$='review_item_block_" + reviewId + "']").html();
    var tempEncode = htmlEncode(temp);
    $('div.review_temp').html(tempEncode);

    $("<img class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("div[id$='review_item_block_" + reviewId + "']").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })

    // $("div[id$='review_item_block_" + reviewId + "']").clone().insertBefore('div.review_temp');
    var ReviewProduct = GetValueQueryString("reviews");

    if (ReviewProduct == "") {
        //EnableDiv("review_item_" + reviewId);
        $.get("ajax_review_post_edit.aspx?revId=" + reviewId + "&reviews=hotels" + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='review_item_block_" + reviewId + "']").html(data);
            $("#form1").find("input,textarea").addClass("review_textbox_style");

            $("#form1").find("select").addClass("review_dropDown_style");

            
        });
    }
    else {
        //EnableDiv("review_item_" + reviewId);

        $.get("ajax_review_post_edit.aspx?revId=" + reviewId + "&reviews=" + ReviewProduct + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            $("div[id$='review_item_block_" + reviewId + "']").html(data);
            $("#form1").find("input,textarea").addClass("review_textbox_style");

            $("#form1").find("select").addClass("review_dropDown_style");

            
        });
    }
}

function UpdatestatusConfirmBox(id, text, reviewId, statusType) {

    //alert("If you want a try, you've got it!");
    var status;
    var statusBin;
    switch (statusType) {
        case "approve":
            status = true;
            statusBin = true;
            break;
        case "bin":
            status = false;
            statusBin = false;
            break;
        case "unapprove":
            status = false;
            statusBin = true;
            break;
        case "unbin":
            status = false;
            statusBin = true;
            break;

    }

    $("<img style=\"margin:15px 0px 0px 0px; \" class=\"img_progress\" src=\"../../images/progress.gif\" alt=\"Progress\" />").appendTo("div[id$='review_item_" + reviewId + "']").ajaxStart(function () {
        $(this).show();
    }).ajaxStop(function () {

        $(this).remove();

    })
    var ReviewProduct = GetValueQueryString("reviews");
    if (ReviewProduct == "") {
        $.get("ajax_review_post_detail_update.aspx?revId=" + reviewId + "&reviews=hotels&status=" + status + "&status_bin=" + statusBin + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
            alert(data);
            if (data == "Success") {

                $("div[id$='review_item_block_" + reviewId + "']").remove();
            }
        });

    } else {

        $.get("ajax_review_post_detail_update.aspx?revId=" + reviewId + "&reviews=" + ReviewProduct + "&status=" + status + "&status_bin=" + statusBin + GetQuerystringProductAndSupplierForBluehouseManage("append"), function (data) {
           
            if (data == "Success") {
                $("div[id$='review_item_block_" + reviewId + "']").remove();
            }

        });
    }


//    $("#" + id + "").fastConfirm({
//        position: "right",
//        questionText: text,
//        onProceed: function (trigger) {
//            


//        },
//        onCancel: function (trigger) {
//            //alert("Erm. In fact, you were already trying it, don't you think?");
//        }
//    });

}

function SetRadioValue(id_element, value) {

    var element = document.getElementById(id_element);
    var arryrario = element.getElementsByTagName("input");

    for (i = 0; i <= arryrario.length - 1; i++) {
        if (arryrario[i].type == 'radio') {
            if (arryrario[i].value == value) {
                arryrario[i].checked = true;
            }

        }
    }
}



function GetValueQueryString(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}




function ReviewActiveLink_Type(linkId) {

    $("#" + linkId).unbind("hover");
    $("#" + linkId).unbind("mouseout");

    $("#review_left_type a").hover(function () {
        if ($(this).attr("id") != linkId) {
            $(this).css("background-color", "#d8dfea");
        }

    });

    $("#review_left_type a").mouseout(function () {
        if ($(this).attr("id") != linkId) {
            $(this).css("background-color", "#ffffff");
        }
    });

    $("#review_left_type a").filter(function (index) {
        return $(this).attr("id") == linkId;
    }).css({
        "background-color": "#d8dfea", "font-weight": "bold"
    });

    $("#review_left_type a").filter(function (index) {
        return $(this).attr("id") != linkId;
    }).css({
        "background-color": "#ffffff", "font-weight": "normal"
    });

}

function ReviewActiveLink(linkId) {

    $("#" + linkId).unbind("hover");
    $("#" + linkId).unbind("mouseout");

    $("#review_left_Product a").hover(function () {
        if ($(this).attr("id") != linkId) {
            $(this).css("background-color", "#d8dfea");
        }

    });

    $("#review_left_Product a").mouseout(function () {
        if ($(this).attr("id") != linkId) {
            $(this).css("background-color", "#ffffff");
        }
    });

    $("#review_left_Product a").filter(function (index) {
        return $(this).attr("id") == linkId;
    }).css({
        "background-color": "#d8dfea", "font-weight": "bold"
    });

    $("#review_left_Product a").filter(function (index) {
        return $(this).attr("id") != linkId;
    }).css({
        "background-color": "#ffffff", "font-weight": "normal"
    });


}


$(document).ready(function () {

    var ReviewProduct = GetValueQueryString("reviews");
    if (ReviewProduct == "") {
        $("#review_left_Product a").hover(function () {
            if ($(this).attr("id") != "h_hotels") {
                $(this).css("background-color", "#eceff5");
            }

        });

        $("#review_left_Product a").mouseout(function () {
            if ($(this).attr("id") != "h_hotels") {
                $(this).css("background-color", "#ffffff");
            }
        });

        $("#review_left_Product a").filter(function (index) {
            return index == 0;
        }).css({
            "background-color": "#d8dfea", "font-weight": "bold"
        });

    } else {

        $("#review_left_Product a").hover(function () {
            if ($(this).attr("id") != "h_" + ReviewProduct) {
                $(this).css("background-color", "#eceff5");
            }

        });

        $("#review_left_Product a").mouseout(function () {
            if ($(this).attr("id") != "h_" + ReviewProduct) {
                $(this).css("background-color", "#ffffff");
            }
        });

        $("#review_left_Product a").filter(function (index) {
            return $(this).attr("id") == "h_" + ReviewProduct;
        }).css({
            "background-color": "#d8dfea", "font-weight": "bold"
        });
    }

});

$(document).ready(function () {

    var ReviewType = GetValueQueryString("revType");
    if (ReviewType == "") {
        $("#review_left_type a").hover(function () {
            if ($(this).attr("id") != "h_waiting") {
                $(this).css("background-color", "#eceff5");
            }

        });

        $("#review_left_type a").mouseout(function () {
            if ($(this).attr("id") != "h_waiting") {
                $(this).css("background-color", "#ffffff");
            }
        });

        $("#review_left_type a").filter(function (index) {
            return index == 1;
        }).css({
            "background-color": "#d8dfea", "font-weight": "bold"
        });

    } else {

        $("#review_left_type a").hover(function () {
            if ($(this).attr("id") != "h_" + ReviewType) {
                $(this).css("background-color", "#eceff5");
            }

        });

        $("#review_left_type a").mouseout(function () {
            if ($(this).attr("id") != "h_" + ReviewType) {
                $(this).css("background-color", "#ffffff");
            }
        });

        $("#review_left_type a").filter(function (index) {
            return $(this).attr("id") == "h_" + ReviewType;
        }).css({
            "background-color": "#d8dfea", "font-weight": "bold"
        });
    }

});