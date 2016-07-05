/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: PT_BR
 */

jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        return this.optional(element) || /^[0-3][0-9]\/[0-1][0-9]\/[0-9]{4} [0-2][0-9]:[0-5][0-9]?$/.test(value);
    },
    number: function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    }
});