(function() {
    document.querySelector('input').addEventListener('keydown', function(e) {
        if (e.keyCode === 13) {
            // можете делать все что угодно со значением текстового поля
            // document.getElementById("search-logo-box").style.paddingTop = "5%";
            if (document.querySelector('input').value == "") {
            	document.querySelector('input').placeholder = 'Нельзя отправлять пустой запрос';
            } else {
            	// document.querySelector('input').placeholder = 'Введите ваш вопрос здесь';
                document.getElementById("search-logo-box").classList.add("search-logo-box-after");
                var main = document.getElementById("main");
                var str = '<div class="quote-box" id="quote"><div class="quote-box_btn" id="btn"><h3>Следующая цитата</h3></div></div>';
                main.innerHTML += str;
                var quote = document.getElementById("quote");
                let counter = 0;
                let arr = ["Яблоко залупы Сергеевны","Сика бомжа","Траханье котов 9"];
                document.getElementById("btn").addEventListener('click', function(e){
                    console.log(arr.lenght);
                    while (counter < 10) {
                        console.log(arr[counter]);
                        var str2 = '<h1>'+ arr[counter] +'</h1>';
                        quote.innerHTML += str2;
                        counter++;
                    }
                })
            }
        }
        else if (e.keyCode === 27){
        	document.querySelector('input').blur();
        	document.querySelector('input').value = "";
        }
    });
})();