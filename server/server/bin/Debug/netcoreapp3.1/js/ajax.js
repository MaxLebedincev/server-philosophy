$(document).ready(function(){
    $("#ButtonOutput").click(
        function(){
            NewQuery();
        }
    );
	
	$("#ButtonOutput2").click(
        function(){
            Query();
        }
    );
});


function NewQuery(){

	$.ajax({
	url: '/index.html',
	method: 'get',
	cache: false
	});

}

function Query() {
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

function CheckAndFinal(ResultForm, Form, url) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "html",
        data: $("#"+Form).serialize(),
        success: function(response) {
            result = response;
            console.log("YES");
            $('#ResultFinal').html(result);
        },
        error: function(response) {
            $('#ResultFinal').html('Ошибка. Данные не отправлены.');
        }
    }).then(function(e){
        let res = e;
    })
}