/* =============== Mobile Navigation ============== */
$(function () {
	$('.menu-toggle').click(function () {
		// show and hide Mobile Toggle Btn
		$(".nav-menu-link").toggleClass("mobile-nav");
		$(this).toggleClass("is-active");
	});
});

/* ================= password show/hide =============== */
$(document).ready(function () {
	$("body").on('click', '.toggle-password', function () {
		var input = $($(this).attr("toggle"));
		if (input.attr("type") === "password") {
			input.attr("type", "text");
		} else {
			input.attr("type", "password");
		}
	});
});

$("body").on("submit", "#Reject", function () {
	return confirm("Are you sure you want to unpublished?");
});