$(function () {
  
    //$("#date").datepicker();
  
    //$("#winds").click(function (event) {

      

    //});

  
   

    var ajaxFormSubmit = function () {
        
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-jtv-target"));
            //$target.replaceWith(data);
            var $newhtml = $(data);   
            $target.replaceWith($newhtml);
            $newhtml.effect("highlight");
           
        });
        return false;
    };


  

    var submitAutoCompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();

    };

    var createAutocomplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-jtv-autocomplete"),
            select: submitAutoCompleteForm
        };
        $input.autocomplete(options);

    };

    var getpage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-jtv-target");
            $(target).replaceWith(data);
        });
        return false;
    };

  
    //var sampleMe = function () {

        
    //    var $nme = $(this);
    //   //window.alert($nme.attr("data-value"));
        
    //};

    //$("#reg1").click(function () {
    //    var bt = $(this);
    //    bt.css("background-color", "Blue")
    //    $("#reg2").css("background-color", "None")
    //    $("#reg3").css("background-color", "None")
    //});


    $("#fin").keypress(function(event) {
        var myval = $("#fin").val();
        var amt = $("#amt").val();

        $("#exped").val((myval*amt));

    });

    $("#Phy").keypress(function (event) {

        var ival = $("#Phy").val();
        var amt1 = $("#amt").val();
        $("#valuea").val((ival * amt1));
    
    });

  

    function doHourglass() {
        document.body.style.cursor = 'wait';
    }
  

   // $('.datepicker').datepicker();


    //$("button[data-jtv-sample='true']").click(sampleMe);
    $("form[data-jtv-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-jtv- ]").each(createAutocomplete);
    $(".main-content").on("click", ".pagedList a", getpage);
   


    



  
});

