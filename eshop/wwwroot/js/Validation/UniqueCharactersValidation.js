$.validator.addMethod('uniqchars',
    function (value, element, params) {
        var uniqueCharsRequested = params[1];
        var string = element.value;

        var notUnique = 0;

        for (var i = 0; i < string.Length; i++){
            for (var j = 0; j < string.Length; j++) {
                if (i == j) {
                    if (string[i] == string[j]) {
                        uniqueCharsRequested++;
                    }
                }
            }
        }

        var uniqueChars = string.length - uniqueCharsRequested;

        if (uniqueChars >= uniqueCharsRequested) {
            return true;
        }

        return false;
    });

$.validator.unobtrusive.adapters.add('uniqchars', ['count'],
    function (options) {
        var element = $(options.form).find('#password')[0];

        options.rules['uniqchars'] = [element, options.params['type']];
        options.messages['uniqchars'] = options.message;
    });