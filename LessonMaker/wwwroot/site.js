const uriGetLessons = "api/lessons";
const uriGetFeaturedLessons = "api/featured-lessons";
const uriPostLesson = "api/lesson/post";
const uriEditLesson = "api/lesson";
const uriUpvoteLesson = "api/upvote";
const uriDownvoteLesson = "api/downvote";
const uriDeleteLesson = "api/delete";

let lessons = null;
let featuredLessons = null;

function getCount(data) {
    const el = $("#counter");
    let name = "lesson";
    if (data) {
        if (data > 1) {
            name = "lessons";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

function getFeaturedCount(featuredData) {
    const ef = $("#counter-featured");
    let name = "lesson";
    if (featuredData) {
        if (featuredData > 1) {
            name = "lessons";
        }
        ef.text(featuredData + " " + name);
    } else {
        ef.text("No " + name);
    }
}

$(document).ready(function () {
    getData();
    getFeaturedData();
});

function getFeaturedData() {
    $.ajax({
        type: "GET",
        url: uriGetFeaturedLessons,
        cache: false,
        success: function (featuredData) {
            const tBody = $("#featured-lessons");

            $(tBody).empty();

            getFeaturedCount(featuredData.length);

            $.each(featuredData, function (key, lesson) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(lesson.title))
                    .append($("<td></td>").text(lesson.content))
                    .append($("<td></td>").text(lesson.author))
                    .append($("<td></td>").text(lesson.creationDate))
                    .append($("<td></td>").text(lesson.votes));

                tr.appendTo(tBody);
            });

            featuredLessons = featuredData;
        }
    });
}

function getData() {
    $.ajax({
        type: "GET",
        url: uriGetLessons,
        cache: false,
        success: function (data) {
            const tBody = $("#lessons");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, lesson) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(lesson.title))
                    .append($("<td></td>").text(lesson.content))
                    .append($("<td></td>").text(lesson.author))
                    .append($("<td></td>").text(lesson.creationDate))
                    .append($("<td></td>").text(lesson.votes))
                    .append(
                        $("<td></td>").append(
                            $("<button>Upvote</button>").on("click", function () {
                                upvoteLesson(lesson.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Downvote</button>").on("click", function () {
                                downvoteLesson(lesson.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editLesson(lesson.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteLesson(lesson.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            lessons = data;
        }
    });
}

function addLesson() {
    const lesson = {
        title: $("#add-title").val(),
        content: $("#add-content").val(),
        author: $("#add-author").val(),
        votes: 0
    };
    if (lesson.title != "" && lesson.content != "" && lesson.author != "") {
        $.ajax({
            type: "POST",
            accepts: "application/json",
            url: uriPostLesson,
            contentType: "application/json",
            data: JSON.stringify(lesson),
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Something went wrong!");
            },
            success: function (result) {
                getData();
                $("#add-title").val("");
                $("#add-content").val("");
                $("#add-author").val("");
                $("#error-message").text("");
            }
        });
    }
    else {
        $("#error-message").text("You must fill out the form before adding a new lesson.");
    }

}

function deleteLesson(id) {
    $.ajax({
        url: uriDeleteLesson + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
            getFeaturedData();
        }
    });
}

function editLesson(id) {
    $.each(lessons, function (key, lesson) {
        if (lesson.id === id) {
            $("#edit-id").val(lesson.id);
            $("#edit-title").val(lesson.title);
            $("#edit-content").val(lesson.content);
            $("#edit-author").val(lesson.author);
        }
    });
    $("#spoiler").css({ display: "block" });
}

function upvoteLesson(id) {
    $.ajax({
        url: uriUpvoteLesson + "/" + id,
        type: "PUT",
        success: function (result) {
            getData();
            getFeaturedData();
        }
    });
}

function downvoteLesson(id) {
    $.ajax({
        url: uriDownvoteLesson + "/" + id,
        type: "PUT",
        success: function (result) {
            getData();
            getFeaturedData();
        }
    });
}

$(".my-form").on("submit", function () {
    const lesson = {
        id: $("#edit-id").val(),
        title: $("#edit-title").val(),
        content: $("#edit-content").val(),
        author: $("#edit-author").val()
    };

    $.ajax({
        url: uriEditLesson + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(lesson),
        success: function (result) {
            getData();
            getFeaturedData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}