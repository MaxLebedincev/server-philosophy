$(document).ready(function(){
    $("#ButtonOutput").click(
        function(){
            GETQuery();
        }
    );
	
	$("#ButtonOutput2").click(
        function(){
            POSTQuery();
        }
    );
});


function GETQuery(){

	$.ajax({
	url: '/index.html',
	method: 'get',
	cache: false
	});

}

function POSTQuery() {
    $.ajax({
        url: '/index.html',
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ 
                SendInfo: 'asdas' 
              }),
        success: function(response) {
            result = response;
            console.log("YES");
        },
        error: function(response) {
			console.log("Ошибка. Данные не отправлены.");
        }
    });
}
