// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

//$("#submit").click(function (e) {
//    e.preventDefault();
//    //var formData = new FormData();
//    //$("input[name='Id']").each(function (i) {
//    //    var Id = $(this).val();
//    //    formData.append("ContactEmailAddress[" + i + "].EmailType", Id);
//    //});
//    //$("input[name='EmailType']").each(function (i) {
//    //    var EmailType = $(this).val();
//    //    formData.append("ContactEmailAddress[" + i + "].EmailType", EmailType);
//    //});
//    //$("input[name='EmailAddress']").each(function (i) {
//    //    var EmailAddress = $(this).val();
//    //    formData.append("ContactEmailAddress[" + i + "].EmailAddress", EmailAddress);
//    //});
//    $.ajax({
//        method: 'post',
//        url: 'Contacts/Edit/1',
//        data: formData,
//        processData: false,
//        contentType: false,
//        success: function () {
//        }
//    });
//});

jQuery('body').on('input', 'input.SearchString', function (form) {
    var formData = new FormData();
    //$("input.SearchString").val();
    var searchString = $("input[name='SearchString']").val();
    formData.append("SearchString", searchString);
    try {
    $.ajax({
        type: 'POST',
        //url: form.action,
        url: 'Contacts/Index/?searchstring=' + searchString,
        //data: new FormData(form),
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            $('#view-all').html(res.html);
        },
        error: function (err) {
            console.log(err)
        }
    })
  } catch (ex) {
    console.log(ex)
  }
//prevent default form submit event
  return false;
});

var counter = 2;

function deleteTr(index) {
    if (counter > 1) {
        $('#tablerow' + index).remove();
        counter--;
    }
    return false;
}

$("#addItem").click(function () {
    $.ajax({
        url: this.href,
        cache: false,
        success: function (html) {
            counter++;
            var htmlEmaiItem = '';
            htmlEmaiItem += '                    <tr id="tablerow' + counter + '" class="emailRow">';
            htmlEmaiItem += '                        <td width="40%" align="left">';
            htmlEmaiItem += '                            <input type="hidden" data-val="true" data-val-required="The Id field is required." id="item_Id" name="item.Id" value="0" />';
            htmlEmaiItem += '                            <select id="item_EmailType" name="item.EmailType" class="form-control col-md-auto">';
            //htmlEmaiItem += '                                <option value="" disabled hidden selected>Select Type</option>';
            htmlEmaiItem += '                                <option value="Business" selected>Business</option>';
            htmlEmaiItem += '                                <option value="Personal">Personal</option>';
            htmlEmaiItem += '                            </select>';
            htmlEmaiItem += '                        </td>';
            htmlEmaiItem += '                        <td align="left">';
            htmlEmaiItem += '                            <input class="form-control col-md-auto" type="email" data-val="true" data-val-length="Email address cannot be longer than 150 characters." data-val-length-max="150" data-val-maxlength="The field Email Address must be a string or array type with a maximum length of &#x27;150&#x27;." data-val-maxlength-max="150" data-val-regex="Incorrect Email Format" data-val-regex-pattern="[a-zA-Z0-9._%&#x2B;-]&#x2B;@[a-zA-Z0-9.-]&#x2B;\.[a-zA-Z]{2,4}" data-val-required="The Email Address field is required." id="item_EmailAddress" maxlength="150" name="item.EmailAddress" value="" />';
            htmlEmaiItem += '                            <span class="text-danger field-validation-valid" data-valmsg-for="item.EmailAddress" data-valmsg-replace="true"></span>';
            htmlEmaiItem += '                        </td>';
            htmlEmaiItem += '                        <td align="right">';
            //htmlEmaiItem += '                           <a href="#" class="deleteRow text-danger ml-1" title="Remove"><i class="fas fa-trash-alt fa-sm" style="border:none"></i></a>';
            htmlEmaiItem += '                           <a href="#" class="deleteItem text-danger ml-1" title="Remove" onclick="deleteTr(' + counter + ')"><i class="fas fa-trash-alt fa-sm" style="border:none"></i></a>';
            htmlEmaiItem += '                        </td>';
            htmlEmaiItem += '                    </tr>';

            $("#tEmailAddresses").append(htmlEmaiItem);
        }
    });
    return false;
});

$("a.deleteRow").on("click", function () {
    //$(this).parents("div.emailEditRow:first").remove();
    $(this).parents("tr.emailRow:first").remove();
    return false;
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            //$("nav .navbar").hide();
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            $(".navbar").hide();
            $(".footer").hide();
            // to make popup draggable
        //    $('.modal-dialog').draggable({
        //        handle: ".modal-header"
        //    });
        }
    })
}

AddEmailItem = form => {
    //$('input[name="AddNewEmail"]').val("true");
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

ContactSearch = form => {
    try {
        $.ajax({
            type: 'GET',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#view-all').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    //prevent default form submit event
    return false;
}

SubmitContactPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

ContactDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}