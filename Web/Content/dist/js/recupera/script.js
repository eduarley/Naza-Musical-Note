// JQUERY
$(function() {
	
	//var images = ['https://wallpapersite.com/images/pages/pic_w/1063.jpg', 'http://www.hdwallpaper.nu/wp-content/uploads/2017/04/PLAYERUNKNOWNS-BATTLEGROUNDS-12937710.jpg', 'https://www.hdwallpapers.in/walls/overwatch_4k-HD.jpg', 'https://images.alphacoders.com/186/186993.jpg', 'https://images4.alphacoders.com/602/thumb-1920-602788.png'];
	var images = ['../Content/dist/img/login/fondologinblur.png'];
   $('#container').append('<style>#container, .acceptContainer:before, #logoContainer:before {background: url(' + images[Math.floor(Math.random() * images.length)] + ') center fixed }');
	
	
	
	
				setTimeout(function() {
					$('.acceptContainer').transition({height: '431.5px'});
					setTimeout(function() {
						$('.acceptContainer').addClass('loadIn');
						setTimeout(function() {
							$('.formDiv, form h1').addClass('loadIn');
						}, 500)
					}, 500)
				}, 300)
	

					/*!
				 * jQuery Validation Plugin v1.17.0
				 *
				 * https://jqueryvalidation.org/
				 *
				 * Copyright (c) 2017 Jörn Zaefferer
				 * Released under the MIT license
				 */
					(function (factory) {
						if (typeof define === "function" && define.amd) {
							define(["jquery"], factory);
						} else if (typeof module === "object" && module.exports) {
							module.exports = factory(require("jquery"));
						} else {
							factory(jQuery);
						}
					}(function ($) {

						$.extend($.fn, {

							// https://jqueryvalidation.org/validate/
							validate: function (options) {

								// If nothing is selected, return nothing; can't chain anyway
								if (!this.length) {
									if (options && options.debug && window.console) {
										console.warn("Nothing selected, can't validate, returning nothing.");
									}
									return;
								}

								// Check if a validator for this form was already created
								var validator = $.data(this[0], "validator");
								if (validator) {
									return validator;
								}

								// Add novalidate tag if HTML5.
								this.attr("novalidate", "novalidate");

								validator = new $.validator(options, this[0]);
								$.data(this[0], "validator", validator);

								if (validator.settings.onsubmit) {

									this.on("click.validate", ":submit", function (event) {

										// Track the used submit button to properly handle scripted
										// submits later.
										validator.submitButton = event.currentTarget;

										// Allow suppressing validation by adding a cancel class to the submit button
										if ($(this).hasClass("cancel")) {
											validator.cancelSubmit = true;
										}

										// Allow suppressing validation by adding the html5 formnovalidate attribute to the submit button
										if ($(this).attr("formnovalidate") !== undefined) {
											validator.cancelSubmit = true;
										}
									});

									// Validate the form on submit
									this.on("submit.validate", function (event) {
										if (validator.settings.debug) {

											// Prevent form submit to be able to see console output
											event.preventDefault();
										}
										function handle() {
											var hidden, result;

											// Insert a hidden input as a replacement for the missing submit button
											// The hidden input is inserted in two cases:
											//   - A user defined a `submitHandler`
											//   - There was a pending request due to `remote` method and `stopRequest()`
											//     was called to submit the form in case it's valid
											if (validator.submitButton && (validator.settings.submitHandler || validator.formSubmitted)) {
												hidden = $("<input type='hidden'/>")
													.attr("name", validator.submitButton.name)
													.val($(validator.submitButton).val())
													.appendTo(validator.currentForm);
											}

											if (validator.settings.submitHandler) {
												result = validator.settings.submitHandler.call(validator, validator.currentForm, event);
												if (hidden) {

													// And clean up afterwards; thanks to no-block-scope, hidden can be referenced
													hidden.remove();
												}
												if (result !== undefined) {
													return result;
												}
												return false;
											}
											return true;
										}

										// Prevent submit for invalid forms or custom submit handlers
										if (validator.cancelSubmit) {
											validator.cancelSubmit = false;
											return handle();
										}
										if (validator.form()) {
											if (validator.pendingRequest) {
												validator.formSubmitted = true;
												return false;
											}
											return handle();
										} else {
											validator.focusInvalid();
											return false;
										}
									});
								}

								return validator;
							},

							// https://jqueryvalidation.org/valid/
							valid: function () {
								var valid, validator, errorList;

								if ($(this[0]).is("form")) {
									valid = this.validate().form();
								} else {
									errorList = [];
									valid = true;
									validator = $(this[0].form).validate();
									this.each(function () {
										valid = validator.element(this) && valid;
										if (!valid) {
											errorList = errorList.concat(validator.errorList);
										}
									});
									validator.errorList = errorList;
								}
								return valid;
							},

							// https://jqueryvalidation.org/rules/
							rules: function (command, argument) {
								var element = this[0],
									settings, staticRules, existingRules, data, param, filtered;

								// If nothing is selected, return empty object; can't chain anyway
								if (element == null) {
									return;
								}

								if (!element.form && element.hasAttribute("contenteditable")) {
									element.form = this.closest("form")[0];
									element.name = this.attr("name");
								}

								if (element.form == null) {
									return;
								}

								if (command) {
									settings = $.data(element.form, "validator").settings;
									staticRules = settings.rules;
									existingRules = $.validator.staticRules(element);
									switch (command) {
										case "add":
											$.extend(existingRules, $.validator.normalizeRule(argument));

											// Remove messages from rules, but allow them to be set separately
											delete existingRules.messages;
											staticRules[element.name] = existingRules;
											if (argument.messages) {
												settings.messages[element.name] = $.extend(settings.messages[element.name], argument.messages);
											}
											break;
										case "remove":
											if (!argument) {
												delete staticRules[element.name];
												return existingRules;
											}
											filtered = {};
											$.each(argument.split(/\s/), function (index, method) {
												filtered[method] = existingRules[method];
												delete existingRules[method];
											});
											return filtered;
									}
								}

								data = $.validator.normalizeRules(
									$.extend(
										{},
										$.validator.classRules(element),
										$.validator.attributeRules(element),
										$.validator.dataRules(element),
										$.validator.staticRules(element)
									), element);

								// Make sure required is at front
								if (data.required) {
									param = data.required;
									delete data.required;
									data = $.extend({ required: param }, data);
								}

								// Make sure remote is at back
								if (data.remote) {
									param = data.remote;
									delete data.remote;
									data = $.extend(data, { remote: param });
								}

								return data;
							}
						});

						// Custom selectors
						$.extend($.expr.pseudos || $.expr[":"], {		// '|| $.expr[ ":" ]' here enables backwards compatibility to jQuery 1.7. Can be removed when dropping jQ 1.7.x support

							// https://jqueryvalidation.org/blank-selector/
							blank: function (a) {
								return !$.trim("" + $(a).val());
							},

							// https://jqueryvalidation.org/filled-selector/
							filled: function (a) {
								var val = $(a).val();
								return val !== null && !!$.trim("" + val);
							},

							// https://jqueryvalidation.org/unchecked-selector/
							unchecked: function (a) {
								return !$(a).prop("checked");
							}
						});

						// Constructor for validator
						$.validator = function (options, form) {
							this.settings = $.extend(true, {}, $.validator.defaults, options);
							this.currentForm = form;
							this.init();
						};

						// https://jqueryvalidation.org/jQuery.validator.format/
						$.validator.format = function (source, params) {
							if (arguments.length === 1) {
								return function () {
									var args = $.makeArray(arguments);
									args.unshift(source);
									return $.validator.format.apply(this, args);
								};
							}
							if (params === undefined) {
								return source;
							}
							if (arguments.length > 2 && params.constructor !== Array) {
								params = $.makeArray(arguments).slice(1);
							}
							if (params.constructor !== Array) {
								params = [params];
							}
							$.each(params, function (i, n) {
								source = source.replace(new RegExp("\\{" + i + "\\}", "g"), function () {
									return n;
								});
							});
							return source;
						};

						$.extend($.validator, {

							defaults: {
								messages: {},
								groups: {},
								rules: {},
								errorClass: "error",
								pendingClass: "pending",
								validClass: "valid",
								errorElement: "label",
								focusCleanup: false,
								focusInvalid: true,
								errorContainer: $([]),
								errorLabelContainer: $([]),
								onsubmit: true,
								ignore: ":hidden",
								ignoreTitle: false,
								onfocusin: function (element) {
									this.lastActive = element;

									// Hide error label and remove error class on focus if enabled
									if (this.settings.focusCleanup) {
										if (this.settings.unhighlight) {
											this.settings.unhighlight.call(this, element, this.settings.errorClass, this.settings.validClass);
										}
										this.hideThese(this.errorsFor(element));
									}
								},
								onfocusout: function (element) {
									if (!this.checkable(element) && (element.name in this.submitted || !this.optional(element))) {
										this.element(element);
									}
								},
								onkeyup: function (element, event) {

									// Avoid revalidate the field when pressing one of the following keys
									// Shift       => 16
									// Ctrl        => 17
									// Alt         => 18
									// Caps lock   => 20
									// End         => 35
									// Home        => 36
									// Left arrow  => 37
									// Up arrow    => 38
									// Right arrow => 39
									// Down arrow  => 40
									// Insert      => 45
									// Num lock    => 144
									// AltGr key   => 225
									var excludedKeys = [
										16, 17, 18, 20, 35, 36, 37,
										38, 39, 40, 45, 144, 225
									];

									if (event.which === 9 && this.elementValue(element) === "" || $.inArray(event.keyCode, excludedKeys) !== -1) {
										return;
									} else if (element.name in this.submitted || element.name in this.invalid) {
										this.element(element);
									}
								},
								onclick: function (element) {

									// Click on selects, radiobuttons and checkboxes
									if (element.name in this.submitted) {
										this.element(element);

										// Or option elements, check parent select in that case
									} else if (element.parentNode.name in this.submitted) {
										this.element(element.parentNode);
									}
								},
								highlight: function (element, errorClass, validClass) {
									if (element.type === "radio") {
										this.findByName(element.name).addClass(errorClass).removeClass(validClass);
									} else {
										$(element).addClass(errorClass).removeClass(validClass);
									}
								},
								unhighlight: function (element, errorClass, validClass) {
									if (element.type === "radio") {
										this.findByName(element.name).removeClass(errorClass).addClass(validClass);
									} else {
										$(element).removeClass(errorClass).addClass(validClass);
									}
								}
							},

							// https://jqueryvalidation.org/jQuery.validator.setDefaults/
							setDefaults: function (settings) {
								$.extend($.validator.defaults, settings);
							},

							messages: {
								required: "This field is required.",
								remote: "Please fix this field.",
								email: "Please enter a valid email address.",
								url: "Please enter a valid URL.",
								date: "Please enter a valid date.",
								dateISO: "Please enter a valid date (ISO).",
								number: "Please enter a valid number.",
								digits: "Please enter only digits.",
								equalTo: "Please enter the same value again.",
								maxlength: $.validator.format("Please enter no more than {0} characters."),
								minlength: $.validator.format("Please enter at least {0} characters."),
								rangelength: $.validator.format("Please enter a value between {0} and {1} characters long."),
								range: $.validator.format("Please enter a value between {0} and {1}."),
								max: $.validator.format("Please enter a value less than or equal to {0}."),
								min: $.validator.format("Please enter a value greater than or equal to {0}."),
								step: $.validator.format("Please enter a multiple of {0}.")
							},

							autoCreateRanges: false,

							prototype: {

								init: function () {
									this.labelContainer = $(this.settings.errorLabelContainer);
									this.errorContext = this.labelContainer.length && this.labelContainer || $(this.currentForm);
									this.containers = $(this.settings.errorContainer).add(this.settings.errorLabelContainer);
									this.submitted = {};
									this.valueCache = {};
									this.pendingRequest = 0;
									this.pending = {};
									this.invalid = {};
									this.reset();

									var groups = (this.groups = {}),
										rules;
									$.each(this.settings.groups, function (key, value) {
										if (typeof value === "string") {
											value = value.split(/\s/);
										}
										$.each(value, function (index, name) {
											groups[name] = key;
										});
									});
									rules = this.settings.rules;
									$.each(rules, function (key, value) {
										rules[key] = $.validator.normalizeRule(value);
									});

									function delegate(event) {

										// Set form expando on contenteditable
										if (!this.form && this.hasAttribute("contenteditable")) {
											this.form = $(this).closest("form")[0];
											this.name = $(this).attr("name");
										}

										var validator = $.data(this.form, "validator"),
											eventType = "on" + event.type.replace(/^validate/, ""),
											settings = validator.settings;
										if (settings[eventType] && !$(this).is(settings.ignore)) {
											settings[eventType].call(validator, this, event);
										}
									}

									$(this.currentForm)
										.on("focusin.validate focusout.validate keyup.validate",
											":text, [type='password'], [type='file'], select, textarea, [type='number'], [type='search'], " +
											"[type='tel'], [type='url'], [type='email'], [type='datetime'], [type='date'], [type='month'], " +
											"[type='week'], [type='time'], [type='datetime-local'], [type='range'], [type='color'], " +
											"[type='radio'], [type='checkbox'], [contenteditable], [type='button']", delegate)

										// Support: Chrome, oldIE
										// "select" is provided as event.target when clicking a option
										.on("click.validate", "select, option, [type='radio'], [type='checkbox']", delegate);

									if (this.settings.invalidHandler) {
										$(this.currentForm).on("invalid-form.validate", this.settings.invalidHandler);
									}
								},

								// https://jqueryvalidation.org/Validator.form/
								form: function () {
									this.checkForm();
									$.extend(this.submitted, this.errorMap);
									this.invalid = $.extend({}, this.errorMap);
									if (!this.valid()) {
										$(this.currentForm).triggerHandler("invalid-form", [this]);
									}
									this.showErrors();
									return this.valid();
								},

								checkForm: function () {
									this.prepareForm();
									for (var i = 0, elements = (this.currentElements = this.elements()); elements[i]; i++) {
										this.check(elements[i]);
									}
									return this.valid();
								},

								// https://jqueryvalidation.org/Validator.element/
								element: function (element) {
									var cleanElement = this.clean(element),
										checkElement = this.validationTargetFor(cleanElement),
										v = this,
										result = true,
										rs, group;

									if (checkElement === undefined) {
										delete this.invalid[cleanElement.name];
									} else {
										this.prepareElement(checkElement);
										this.currentElements = $(checkElement);

										// If this element is grouped, then validate all group elements already
										// containing a value
										group = this.groups[checkElement.name];
										if (group) {
											$.each(this.groups, function (name, testgroup) {
												if (testgroup === group && name !== checkElement.name) {
													cleanElement = v.validationTargetFor(v.clean(v.findByName(name)));
													if (cleanElement && cleanElement.name in v.invalid) {
														v.currentElements.push(cleanElement);
														result = v.check(cleanElement) && result;
													}
												}
											});
										}

										rs = this.check(checkElement) !== false;
										result = result && rs;
										if (rs) {
											this.invalid[checkElement.name] = false;
										} else {
											this.invalid[checkElement.name] = true;
										}

										if (!this.numberOfInvalids()) {

											// Hide error containers on last error
											this.toHide = this.toHide.add(this.containers);
										}
										this.showErrors();

										// Add aria-invalid status for screen readers
										$(element).attr("aria-invalid", !rs);
									}

									return result;
								},

								// https://jqueryvalidation.org/Validator.showErrors/
								showErrors: function (errors) {
									if (errors) {
										var validator = this;

										// Add items to error list and map
										$.extend(this.errorMap, errors);
										this.errorList = $.map(this.errorMap, function (message, name) {
											return {
												message: message,
												element: validator.findByName(name)[0]
											};
										});

										// Remove items from success list
										this.successList = $.grep(this.successList, function (element) {
											return !(element.name in errors);
										});
									}
									if (this.settings.showErrors) {
										this.settings.showErrors.call(this, this.errorMap, this.errorList);
									} else {
										this.defaultShowErrors();
									}
								},

								// https://jqueryvalidation.org/Validator.resetForm/
								resetForm: function () {
									if ($.fn.resetForm) {
										$(this.currentForm).resetForm();
									}
									this.invalid = {};
									this.submitted = {};
									this.prepareForm();
									this.hideErrors();
									var elements = this.elements()
										.removeData("previousValue")
										.removeAttr("aria-invalid");

									this.resetElements(elements);
								},

								resetElements: function (elements) {
									var i;

									if (this.settings.unhighlight) {
										for (i = 0; elements[i]; i++) {
											this.settings.unhighlight.call(this, elements[i],
												this.settings.errorClass, "");
											this.findByName(elements[i].name).removeClass(this.settings.validClass);
										}
									} else {
										elements
											.removeClass(this.settings.errorClass)
											.removeClass(this.settings.validClass);
									}
								},

								numberOfInvalids: function () {
									return this.objectLength(this.invalid);
								},

								objectLength: function (obj) {
									/* jshint unused: false */
									var count = 0,
										i;
									for (i in obj) {

										// This check allows counting elements with empty error
										// message as invalid elements
										if (obj[i] !== undefined && obj[i] !== null && obj[i] !== false) {
											count++;
										}
									}
									return count;
								},

								hideErrors: function () {
									this.hideThese(this.toHide);
								},

								hideThese: function (errors) {
									errors.not(this.containers).text("");
									this.addWrapper(errors).hide();
								},

								valid: function () {
									return this.size() === 0;
								},

								size: function () {
									return this.errorList.length;
								},

								focusInvalid: function () {
									if (this.settings.focusInvalid) {
										try {
											$(this.findLastActive() || this.errorList.length && this.errorList[0].element || [])
												.filter(":visible")
												.focus()

												// Manually trigger focusin event; without it, focusin handler isn't called, findLastActive won't have anything to find
												.trigger("focusin");
										} catch (e) {

											// Ignore IE throwing errors when focusing hidden elements
										}
									}
								},

								findLastActive: function () {
									var lastActive = this.lastActive;
									return lastActive && $.grep(this.errorList, function (n) {
										return n.element.name === lastActive.name;
									}).length === 1 && lastActive;
								},

								elements: function () {
									var validator = this,
										rulesCache = {};

									// Select all valid inputs inside the form (no submit or reset buttons)
									return $(this.currentForm)
										.find("input, select, textarea, [contenteditable]")
										.not(":submit, :reset, :image, :disabled")
										.not(this.settings.ignore)
										.filter(function () {
											var name = this.name || $(this).attr("name"); // For contenteditable
											if (!name && validator.settings.debug && window.console) {
												console.error("%o has no name assigned", this);
											}

											// Set form expando on contenteditable
											if (this.hasAttribute("contenteditable")) {
												this.form = $(this).closest("form")[0];
												this.name = name;
											}

											// Select only the first element for each name, and only those with rules specified
											if (name in rulesCache || !validator.objectLength($(this).rules())) {
												return false;
											}

											rulesCache[name] = true;
											return true;
										});
								},

								clean: function (selector) {
									return $(selector)[0];
								},

								errors: function () {
									var errorClass = this.settings.errorClass.split(" ").join(".");
									return $(this.settings.errorElement + "." + errorClass, this.errorContext);
								},

								resetInternals: function () {
									this.successList = [];
									this.errorList = [];
									this.errorMap = {};
									this.toShow = $([]);
									this.toHide = $([]);
								},

								reset: function () {
									this.resetInternals();
									this.currentElements = $([]);
								},

								prepareForm: function () {
									this.reset();
									this.toHide = this.errors().add(this.containers);
								},

								prepareElement: function (element) {
									this.reset();
									this.toHide = this.errorsFor(element);
								},

								elementValue: function (element) {
									var $element = $(element),
										type = element.type,
										val, idx;

									if (type === "radio" || type === "checkbox") {
										return this.findByName(element.name).filter(":checked").val();
									} else if (type === "number" && typeof element.validity !== "undefined") {
										return element.validity.badInput ? "NaN" : $element.val();
									}

									if (element.hasAttribute("contenteditable")) {
										val = $element.text();
									} else {
										val = $element.val();
									}

									if (type === "file") {

										// Modern browser (chrome & safari)
										if (val.substr(0, 12) === "C:\\fakepath\\") {
											return val.substr(12);
										}

										// Legacy browsers
										// Unix-based path
										idx = val.lastIndexOf("/");
										if (idx >= 0) {
											return val.substr(idx + 1);
										}

										// Windows-based path
										idx = val.lastIndexOf("\\");
										if (idx >= 0) {
											return val.substr(idx + 1);
										}

										// Just the file name
										return val;
									}

									if (typeof val === "string") {
										return val.replace(/\r/g, "");
									}
									return val;
								},

								check: function (element) {
									element = this.validationTargetFor(this.clean(element));

									var rules = $(element).rules(),
										rulesCount = $.map(rules, function (n, i) {
											return i;
										}).length,
										dependencyMismatch = false,
										val = this.elementValue(element),
										result, method, rule, normalizer;

									// Prioritize the local normalizer defined for this element over the global one
									// if the former exists, otherwise user the global one in case it exists.
									if (typeof rules.normalizer === "function") {
										normalizer = rules.normalizer;
									} else if (typeof this.settings.normalizer === "function") {
										normalizer = this.settings.normalizer;
									}

									// If normalizer is defined, then call it to retreive the changed value instead
									// of using the real one.
									// Note that `this` in the normalizer is `element`.
									if (normalizer) {
										val = normalizer.call(element, val);

										if (typeof val !== "string") {
											throw new TypeError("The normalizer should return a string value.");
										}

										// Delete the normalizer from rules to avoid treating it as a pre-defined method.
										delete rules.normalizer;
									}

									for (method in rules) {
										rule = { method: method, parameters: rules[method] };
										try {
											result = $.validator.methods[method].call(this, val, element, rule.parameters);

											// If a method indicates that the field is optional and therefore valid,
											// don't mark it as valid when there are no other rules
											if (result === "dependency-mismatch" && rulesCount === 1) {
												dependencyMismatch = true;
												continue;
											}
											dependencyMismatch = false;

											if (result === "pending") {
												this.toHide = this.toHide.not(this.errorsFor(element));
												return;
											}

											if (!result) {
												this.formatAndAdd(element, rule);
												return false;
											}
										} catch (e) {
											if (this.settings.debug && window.console) {
												console.log("Exception occurred when checking element " + element.id + ", check the '" + rule.method + "' method.", e);
											}
											if (e instanceof TypeError) {
												e.message += ".  Exception occurred when checking element " + element.id + ", check the '" + rule.method + "' method.";
											}

											throw e;
										}
									}
									if (dependencyMismatch) {
										return;
									}
									if (this.objectLength(rules)) {
										this.successList.push(element);
									}
									return true;
								},

								// Return the custom message for the given element and validation method
								// specified in the element's HTML5 data attribute
								// return the generic message if present and no method specific message is present
								customDataMessage: function (element, method) {
									return $(element).data("msg" + method.charAt(0).toUpperCase() +
										method.substring(1).toLowerCase()) || $(element).data("msg");
								},

								// Return the custom message for the given element name and validation method
								customMessage: function (name, method) {
									var m = this.settings.messages[name];
									return m && (m.constructor === String ? m : m[method]);
								},

								// Return the first defined argument, allowing empty strings
								findDefined: function () {
									for (var i = 0; i < arguments.length; i++) {
										if (arguments[i] !== undefined) {
											return arguments[i];
										}
									}
									return undefined;
								},

								// The second parameter 'rule' used to be a string, and extended to an object literal
								// of the following form:
								// rule = {
								//     method: "method name",
								//     parameters: "the given method parameters"
								// }
								//
								// The old behavior still supported, kept to maintain backward compatibility with
								// old code, and will be removed in the next major release.
								defaultMessage: function (element, rule) {
									if (typeof rule === "string") {
										rule = { method: rule };
									}

									var message = this.findDefined(
										this.customMessage(element.name, rule.method),
										this.customDataMessage(element, rule.method),

										// 'title' is never undefined, so handle empty string as undefined
										!this.settings.ignoreTitle && element.title || undefined,
										$.validator.messages[rule.method],
										"<strong>Warning: No message defined for " + element.name + "</strong>"
									),
										theregex = /\$?\{(\d+)\}/g;
									if (typeof message === "function") {
										message = message.call(this, rule.parameters, element);
									} else if (theregex.test(message)) {
										message = $.validator.format(message.replace(theregex, "{$1}"), rule.parameters);
									}

									return message;
								},

								formatAndAdd: function (element, rule) {
									var message = this.defaultMessage(element, rule);

									this.errorList.push({
										message: message,
										element: element,
										method: rule.method
									});

									this.errorMap[element.name] = message;
									this.submitted[element.name] = message;
								},

								addWrapper: function (toToggle) {
									if (this.settings.wrapper) {
										toToggle = toToggle.add(toToggle.parent(this.settings.wrapper));
									}
									return toToggle;
								},

								defaultShowErrors: function () {
									var i, elements, error;
									for (i = 0; this.errorList[i]; i++) {
										error = this.errorList[i];
										if (this.settings.highlight) {
											this.settings.highlight.call(this, error.element, this.settings.errorClass, this.settings.validClass);
										}
										this.showLabel(error.element, error.message);
									}
									if (this.errorList.length) {
										this.toShow = this.toShow.add(this.containers);
									}
									if (this.settings.success) {
										for (i = 0; this.successList[i]; i++) {
											this.showLabel(this.successList[i]);
										}
									}
									if (this.settings.unhighlight) {
										for (i = 0, elements = this.validElements(); elements[i]; i++) {
											this.settings.unhighlight.call(this, elements[i], this.settings.errorClass, this.settings.validClass);
										}
									}
									this.toHide = this.toHide.not(this.toShow);
									this.hideErrors();
									this.addWrapper(this.toShow).show();
								},

								validElements: function () {
									return this.currentElements.not(this.invalidElements());
								},

								invalidElements: function () {
									return $(this.errorList).map(function () {
										return this.element;
									});
								},

								showLabel: function (element, message) {
									var place, group, errorID, v,
										error = this.errorsFor(element),
										elementID = this.idOrName(element),
										describedBy = $(element).attr("aria-describedby");

									if (error.length) {

										// Refresh error/success class
										error.removeClass(this.settings.validClass).addClass(this.settings.errorClass);

										// Replace message on existing label
										error.html(message);
									} else {

										// Create error element
										error = $("<" + this.settings.errorElement + ">")
											.attr("id", elementID + "-error")
											.addClass(this.settings.errorClass)
											.html(message || "");

										// Maintain reference to the element to be placed into the DOM
										place = error;
										if (this.settings.wrapper) {

											// Make sure the element is visible, even in IE
											// actually showing the wrapped element is handled elsewhere
											place = error.hide().show().wrap("<" + this.settings.wrapper + "/>").parent();
										}
										if (this.labelContainer.length) {
											this.labelContainer.append(place);
										} else if (this.settings.errorPlacement) {
											this.settings.errorPlacement.call(this, place, $(element));
										} else {
											place.insertAfter(element);
										}

										// Link error back to the element
										if (error.is("label")) {

											// If the error is a label, then associate using 'for'
											error.attr("for", elementID);

											// If the element is not a child of an associated label, then it's necessary
											// to explicitly apply aria-describedby
										} else if (error.parents("label[for='" + this.escapeCssMeta(elementID) + "']").length === 0) {
											errorID = error.attr("id");

											// Respect existing non-error aria-describedby
											if (!describedBy) {
												describedBy = errorID;
											} else if (!describedBy.match(new RegExp("\\b" + this.escapeCssMeta(errorID) + "\\b"))) {

												// Add to end of list if not already present
												describedBy += " " + errorID;
											}
											$(element).attr("aria-describedby", describedBy);

											// If this element is grouped, then assign to all elements in the same group
											group = this.groups[element.name];
											if (group) {
												v = this;
												$.each(v.groups, function (name, testgroup) {
													if (testgroup === group) {
														$("[name='" + v.escapeCssMeta(name) + "']", v.currentForm)
															.attr("aria-describedby", error.attr("id"));
													}
												});
											}
										}
									}
									if (!message && this.settings.success) {
										error.text("");
										if (typeof this.settings.success === "string") {
											error.addClass(this.settings.success);
										} else {
											this.settings.success(error, element);
										}
									}
									this.toShow = this.toShow.add(error);
								},

								errorsFor: function (element) {
									var name = this.escapeCssMeta(this.idOrName(element)),
										describer = $(element).attr("aria-describedby"),
										selector = "label[for='" + name + "'], label[for='" + name + "'] *";

									// 'aria-describedby' should directly reference the error element
									if (describer) {
										selector = selector + ", #" + this.escapeCssMeta(describer)
											.replace(/\s+/g, ", #");
									}

									return this
										.errors()
										.filter(selector);
								},

								// See https://api.jquery.com/category/selectors/, for CSS
								// meta-characters that should be escaped in order to be used with JQuery
								// as a literal part of a name/id or any selector.
								escapeCssMeta: function (string) {
									return string.replace(/([\\!"#$%&'()*+,./:;<=>?@\[\]^`{|}~])/g, "\\$1");
								},

								idOrName: function (element) {
									return this.groups[element.name] || (this.checkable(element) ? element.name : element.id || element.name);
								},

								validationTargetFor: function (element) {

									// If radio/checkbox, validate first element in group instead
									if (this.checkable(element)) {
										element = this.findByName(element.name);
									}

									// Always apply ignore filter
									return $(element).not(this.settings.ignore)[0];
								},

								checkable: function (element) {
									return (/radio|checkbox/i).test(element.type);
								},

								findByName: function (name) {
									return $(this.currentForm).find("[name='" + this.escapeCssMeta(name) + "']");
								},

								getLength: function (value, element) {
									switch (element.nodeName.toLowerCase()) {
										case "select":
											return $("option:selected", element).length;
										case "input":
											if (this.checkable(element)) {
												return this.findByName(element.name).filter(":checked").length;
											}
									}
									return value.length;
								},

								depend: function (param, element) {
									return this.dependTypes[typeof param] ? this.dependTypes[typeof param](param, element) : true;
								},

								dependTypes: {
									"boolean": function (param) {
										return param;
									},
									"string": function (param, element) {
										return !!$(param, element.form).length;
									},
									"function": function (param, element) {
										return param(element);
									}
								},

								optional: function (element) {
									var val = this.elementValue(element);
									return !$.validator.methods.required.call(this, val, element) && "dependency-mismatch";
								},

								startRequest: function (element) {
									if (!this.pending[element.name]) {
										this.pendingRequest++;
										$(element).addClass(this.settings.pendingClass);
										this.pending[element.name] = true;
									}
								},

								stopRequest: function (element, valid) {
									this.pendingRequest--;

									// Sometimes synchronization fails, make sure pendingRequest is never < 0
									if (this.pendingRequest < 0) {
										this.pendingRequest = 0;
									}
									delete this.pending[element.name];
									$(element).removeClass(this.settings.pendingClass);
									if (valid && this.pendingRequest === 0 && this.formSubmitted && this.form()) {
										$(this.currentForm).submit();

										// Remove the hidden input that was used as a replacement for the
										// missing submit button. The hidden input is added by `handle()`
										// to ensure that the value of the used submit button is passed on
										// for scripted submits triggered by this method
										if (this.submitButton) {
											$("input:hidden[name='" + this.submitButton.name + "']", this.currentForm).remove();
										}

										this.formSubmitted = false;
									} else if (!valid && this.pendingRequest === 0 && this.formSubmitted) {
										$(this.currentForm).triggerHandler("invalid-form", [this]);
										this.formSubmitted = false;
									}
								},

								previousValue: function (element, method) {
									method = typeof method === "string" && method || "remote";

									return $.data(element, "previousValue") || $.data(element, "previousValue", {
										old: null,
										valid: true,
										message: this.defaultMessage(element, { method: method })
									});
								},

								// Cleans up all forms and elements, removes validator-specific events
								destroy: function () {
									this.resetForm();

									$(this.currentForm)
										.off(".validate")
										.removeData("validator")
										.find(".validate-equalTo-blur")
										.off(".validate-equalTo")
										.removeClass("validate-equalTo-blur");
								}

							},

							classRuleSettings: {
								required: { required: true },
								email: { email: true },
								url: { url: true },
								date: { date: true },
								dateISO: { dateISO: true },
								number: { number: true },
								digits: { digits: true },
								creditcard: { creditcard: true }
							},

							addClassRules: function (className, rules) {
								if (className.constructor === String) {
									this.classRuleSettings[className] = rules;
								} else {
									$.extend(this.classRuleSettings, className);
								}
							},

							classRules: function (element) {
								var rules = {},
									classes = $(element).attr("class");

								if (classes) {
									$.each(classes.split(" "), function () {
										if (this in $.validator.classRuleSettings) {
											$.extend(rules, $.validator.classRuleSettings[this]);
										}
									});
								}
								return rules;
							},

							normalizeAttributeRule: function (rules, type, method, value) {

								// Convert the value to a number for number inputs, and for text for backwards compability
								// allows type="date" and others to be compared as strings
								if (/min|max|step/.test(method) && (type === null || /number|range|text/.test(type))) {
									value = Number(value);

									// Support Opera Mini, which returns NaN for undefined minlength
									if (isNaN(value)) {
										value = undefined;
									}
								}

								if (value || value === 0) {
									rules[method] = value;
								} else if (type === method && type !== "range") {

									// Exception: the jquery validate 'range' method
									// does not test for the html5 'range' type
									rules[method] = true;
								}
							},

							attributeRules: function (element) {
								var rules = {},
									$element = $(element),
									type = element.getAttribute("type"),
									method, value;

								for (method in $.validator.methods) {

									// Support for <input required> in both html5 and older browsers
									if (method === "required") {
										value = element.getAttribute(method);

										// Some browsers return an empty string for the required attribute
										// and non-HTML5 browsers might have required="" markup
										if (value === "") {
											value = true;
										}

										// Force non-HTML5 browsers to return bool
										value = !!value;
									} else {
										value = $element.attr(method);
									}

									this.normalizeAttributeRule(rules, type, method, value);
								}

								// 'maxlength' may be returned as -1, 2147483647 ( IE ) and 524288 ( safari ) for text inputs
								if (rules.maxlength && /-1|2147483647|524288/.test(rules.maxlength)) {
									delete rules.maxlength;
								}

								return rules;
							},

							dataRules: function (element) {
								var rules = {},
									$element = $(element),
									type = element.getAttribute("type"),
									method, value;

								for (method in $.validator.methods) {
									value = $element.data("rule" + method.charAt(0).toUpperCase() + method.substring(1).toLowerCase());
									this.normalizeAttributeRule(rules, type, method, value);
								}
								return rules;
							},

							staticRules: function (element) {
								var rules = {},
									validator = $.data(element.form, "validator");

								if (validator.settings.rules) {
									rules = $.validator.normalizeRule(validator.settings.rules[element.name]) || {};
								}
								return rules;
							},

							normalizeRules: function (rules, element) {

								// Handle dependency check
								$.each(rules, function (prop, val) {

									// Ignore rule when param is explicitly false, eg. required:false
									if (val === false) {
										delete rules[prop];
										return;
									}
									if (val.param || val.depends) {
										var keepRule = true;
										switch (typeof val.depends) {
											case "string":
												keepRule = !!$(val.depends, element.form).length;
												break;
											case "function":
												keepRule = val.depends.call(element, element);
												break;
										}
										if (keepRule) {
											rules[prop] = val.param !== undefined ? val.param : true;
										} else {
											$.data(element.form, "validator").resetElements($(element));
											delete rules[prop];
										}
									}
								});

								// Evaluate parameters
								$.each(rules, function (rule, parameter) {
									rules[rule] = $.isFunction(parameter) && rule !== "normalizer" ? parameter(element) : parameter;
								});

								// Clean number parameters
								$.each(["minlength", "maxlength"], function () {
									if (rules[this]) {
										rules[this] = Number(rules[this]);
									}
								});
								$.each(["rangelength", "range"], function () {
									var parts;
									if (rules[this]) {
										if ($.isArray(rules[this])) {
											rules[this] = [Number(rules[this][0]), Number(rules[this][1])];
										} else if (typeof rules[this] === "string") {
											parts = rules[this].replace(/[\[\]]/g, "").split(/[\s,]+/);
											rules[this] = [Number(parts[0]), Number(parts[1])];
										}
									}
								});

								if ($.validator.autoCreateRanges) {

									// Auto-create ranges
									if (rules.min != null && rules.max != null) {
										rules.range = [rules.min, rules.max];
										delete rules.min;
										delete rules.max;
									}
									if (rules.minlength != null && rules.maxlength != null) {
										rules.rangelength = [rules.minlength, rules.maxlength];
										delete rules.minlength;
										delete rules.maxlength;
									}
								}

								return rules;
							},

							// Converts a simple string to a {string: true} rule, e.g., "required" to {required:true}
							normalizeRule: function (data) {
								if (typeof data === "string") {
									var transformed = {};
									$.each(data.split(/\s/), function () {
										transformed[this] = true;
									});
									data = transformed;
								}
								return data;
							},

							// https://jqueryvalidation.org/jQuery.validator.addMethod/
							addMethod: function (name, method, message) {
								$.validator.methods[name] = method;
								$.validator.messages[name] = message !== undefined ? message : $.validator.messages[name];
								if (method.length < 3) {
									$.validator.addClassRules(name, $.validator.normalizeRule(name));
								}
							},

							// https://jqueryvalidation.org/jQuery.validator.methods/
							methods: {

								// https://jqueryvalidation.org/required-method/
								required: function (value, element, param) {

									// Check if dependency is met
									if (!this.depend(param, element)) {
										return "dependency-mismatch";
									}
									if (element.nodeName.toLowerCase() === "select") {

										// Could be an array for select-multiple or a string, both are fine this way
										var val = $(element).val();
										return val && val.length > 0;
									}
									if (this.checkable(element)) {
										return this.getLength(value, element) > 0;
									}
									return value.length > 0;
								},

								// https://jqueryvalidation.org/email-method/
								email: function (value, element) {

									// From https://html.spec.whatwg.org/multipage/forms.html#valid-e-mail-address
									// Retrieved 2014-01-14
									// If you have a problem with this implementation, report a bug against the above spec
									// Or use custom methods to implement your own email validation
									return this.optional(element) || /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/.test(value);
								},

								// https://jqueryvalidation.org/url-method/
								url: function (value, element) {

									// Copyright (c) 2010-2013 Diego Perini, MIT licensed
									// https://gist.github.com/dperini/729294
									// see also https://mathiasbynens.be/demo/url-regex
									// modified to allow protocol-relative URLs
									return this.optional(element) || /^(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})).?)(?::\d{2,5})?(?:[/?#]\S*)?$/i.test(value);
								},

								// https://jqueryvalidation.org/date-method/
								date: function (value, element) {
									return this.optional(element) || !/Invalid|NaN/.test(new Date(value).toString());
								},

								// https://jqueryvalidation.org/dateISO-method/
								dateISO: function (value, element) {
									return this.optional(element) || /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/.test(value);
								},

								// https://jqueryvalidation.org/number-method/
								number: function (value, element) {
									return this.optional(element) || /^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(value);
								},

								// https://jqueryvalidation.org/digits-method/
								digits: function (value, element) {
									return this.optional(element) || /^\d+$/.test(value);
								},

								// https://jqueryvalidation.org/minlength-method/
								minlength: function (value, element, param) {
									var length = $.isArray(value) ? value.length : this.getLength(value, element);
									return this.optional(element) || length >= param;
								},

								// https://jqueryvalidation.org/maxlength-method/
								maxlength: function (value, element, param) {
									var length = $.isArray(value) ? value.length : this.getLength(value, element);
									return this.optional(element) || length <= param;
								},

								// https://jqueryvalidation.org/rangelength-method/
								rangelength: function (value, element, param) {
									var length = $.isArray(value) ? value.length : this.getLength(value, element);
									return this.optional(element) || (length >= param[0] && length <= param[1]);
								},

								// https://jqueryvalidation.org/min-method/
								min: function (value, element, param) {
									return this.optional(element) || value >= param;
								},

								// https://jqueryvalidation.org/max-method/
								max: function (value, element, param) {
									return this.optional(element) || value <= param;
								},

								// https://jqueryvalidation.org/range-method/
								range: function (value, element, param) {
									return this.optional(element) || (value >= param[0] && value <= param[1]);
								},

								// https://jqueryvalidation.org/step-method/
								step: function (value, element, param) {
									var type = $(element).attr("type"),
										errorMessage = "Step attribute on input type " + type + " is not supported.",
										supportedTypes = ["text", "number", "range"],
										re = new RegExp("\\b" + type + "\\b"),
										notSupported = type && !re.test(supportedTypes.join()),
										decimalPlaces = function (num) {
											var match = ("" + num).match(/(?:\.(\d+))?$/);
											if (!match) {
												return 0;
											}

											// Number of digits right of decimal point.
											return match[1] ? match[1].length : 0;
										},
										toInt = function (num) {
											return Math.round(num * Math.pow(10, decimals));
										},
										valid = true,
										decimals;

									// Works only for text, number and range input types
									// TODO find a way to support input types date, datetime, datetime-local, month, time and week
									if (notSupported) {
										throw new Error(errorMessage);
									}

									decimals = decimalPlaces(param);

									// Value can't have too many decimals
									if (decimalPlaces(value) > decimals || toInt(value) % toInt(param) !== 0) {
										valid = false;
									}

									return this.optional(element) || valid;
								},

								// https://jqueryvalidation.org/equalTo-method/
								equalTo: function (value, element, param) {

									// Bind to the blur event of the target in order to revalidate whenever the target field is updated
									var target = $(param);
									if (this.settings.onfocusout && target.not(".validate-equalTo-blur").length) {
										target.addClass("validate-equalTo-blur").on("blur.validate-equalTo", function () {
											$(element).valid();
										});
									}
									return value === target.val();
								},

								// https://jqueryvalidation.org/remote-method/
								remote: function (value, element, param, method) {
									if (this.optional(element)) {
										return "dependency-mismatch";
									}

									method = typeof method === "string" && method || "remote";

									var previous = this.previousValue(element, method),
										validator, data, optionDataString;

									if (!this.settings.messages[element.name]) {
										this.settings.messages[element.name] = {};
									}
									previous.originalMessage = previous.originalMessage || this.settings.messages[element.name][method];
									this.settings.messages[element.name][method] = previous.message;

									param = typeof param === "string" && { url: param } || param;
									optionDataString = $.param($.extend({ data: value }, param.data));
									if (previous.old === optionDataString) {
										return previous.valid;
									}

									previous.old = optionDataString;
									validator = this;
									this.startRequest(element);
									data = {};
									data[element.name] = value;
									$.ajax($.extend(true, {
										mode: "abort",
										port: "validate" + element.name,
										dataType: "json",
										data: data,
										context: validator.currentForm,
										success: function (response) {
											var valid = response === true || response === "true",
												errors, message, submitted;

											validator.settings.messages[element.name][method] = previous.originalMessage;
											if (valid) {
												submitted = validator.formSubmitted;
												validator.resetInternals();
												validator.toHide = validator.errorsFor(element);
												validator.formSubmitted = submitted;
												validator.successList.push(element);
												validator.invalid[element.name] = false;
												validator.showErrors();
											} else {
												errors = {};
												message = response || validator.defaultMessage(element, { method: method, parameters: value });
												errors[element.name] = previous.message = message;
												validator.invalid[element.name] = true;
												validator.showErrors(errors);
											}
											previous.valid = valid;
											validator.stopRequest(element, valid);
										}
									}, param));
									return "pending";
								}
							}

						});

						// Ajax mode: abort
						// usage: $.ajax({ mode: "abort"[, port: "uniqueport"]});
						// if mode:"abort" is used, the previous request on that port (port can be undefined) is aborted via XMLHttpRequest.abort()

						var pendingRequests = {},
							ajax;

						// Use a prefilter if available (1.5+)
						if ($.ajaxPrefilter) {
							$.ajaxPrefilter(function (settings, _, xhr) {
								var port = settings.port;
								if (settings.mode === "abort") {
									if (pendingRequests[port]) {
										pendingRequests[port].abort();
									}
									pendingRequests[port] = xhr;
								}
							});
						} else {

							// Proxy ajax
							ajax = $.ajax;
							$.ajax = function (settings) {
								var mode = ("mode" in settings ? settings : $.ajaxSettings).mode,
									port = ("port" in settings ? settings : $.ajaxSettings).port;
								if (mode === "abort") {
									if (pendingRequests[port]) {
										pendingRequests[port].abort();
									}
									pendingRequests[port] = ajax.apply(this, arguments);
									return pendingRequests[port];
								}
								return ajax.apply(this, arguments);
							};
						}
						return $;
					}));


	/*! jQuery Validation Plugin - v1.17.0 - 7/29/2017
 * https://jqueryvalidation.org/
 * Copyright (c) 2017 Jörn Zaefferer; Licensed MIT */
	!function (a) { "function" == typeof define && define.amd ? define(["jquery"], a) : "object" == typeof module && module.exports ? module.exports = a(require("jquery")) : a(jQuery) }(function (a) { a.extend(a.fn, { validate: function (b) { if (!this.length) return void (b && b.debug && window.console && console.warn("Nothing selected, can't validate, returning nothing.")); var c = a.data(this[0], "validator"); return c ? c : (this.attr("novalidate", "novalidate"), c = new a.validator(b, this[0]), a.data(this[0], "validator", c), c.settings.onsubmit && (this.on("click.validate", ":submit", function (b) { c.submitButton = b.currentTarget, a(this).hasClass("cancel") && (c.cancelSubmit = !0), void 0 !== a(this).attr("formnovalidate") && (c.cancelSubmit = !0) }), this.on("submit.validate", function (b) { function d() { var d, e; return c.submitButton && (c.settings.submitHandler || c.formSubmitted) && (d = a("<input type='hidden'/>").attr("name", c.submitButton.name).val(a(c.submitButton).val()).appendTo(c.currentForm)), !c.settings.submitHandler || (e = c.settings.submitHandler.call(c, c.currentForm, b), d && d.remove(), void 0 !== e && e) } return c.settings.debug && b.preventDefault(), c.cancelSubmit ? (c.cancelSubmit = !1, d()) : c.form() ? c.pendingRequest ? (c.formSubmitted = !0, !1) : d() : (c.focusInvalid(), !1) })), c) }, valid: function () { var b, c, d; return a(this[0]).is("form") ? b = this.validate().form() : (d = [], b = !0, c = a(this[0].form).validate(), this.each(function () { b = c.element(this) && b, b || (d = d.concat(c.errorList)) }), c.errorList = d), b }, rules: function (b, c) { var d, e, f, g, h, i, j = this[0]; if (null != j && (!j.form && j.hasAttribute("contenteditable") && (j.form = this.closest("form")[0], j.name = this.attr("name")), null != j.form)) { if (b) switch (d = a.data(j.form, "validator").settings, e = d.rules, f = a.validator.staticRules(j), b) { case "add": a.extend(f, a.validator.normalizeRule(c)), delete f.messages, e[j.name] = f, c.messages && (d.messages[j.name] = a.extend(d.messages[j.name], c.messages)); break; case "remove": return c ? (i = {}, a.each(c.split(/\s/), function (a, b) { i[b] = f[b], delete f[b] }), i) : (delete e[j.name], f) }return g = a.validator.normalizeRules(a.extend({}, a.validator.classRules(j), a.validator.attributeRules(j), a.validator.dataRules(j), a.validator.staticRules(j)), j), g.required && (h = g.required, delete g.required, g = a.extend({ required: h }, g)), g.remote && (h = g.remote, delete g.remote, g = a.extend(g, { remote: h })), g } } }), a.extend(a.expr.pseudos || a.expr[":"], { blank: function (b) { return !a.trim("" + a(b).val()) }, filled: function (b) { var c = a(b).val(); return null !== c && !!a.trim("" + c) }, unchecked: function (b) { return !a(b).prop("checked") } }), a.validator = function (b, c) { this.settings = a.extend(!0, {}, a.validator.defaults, b), this.currentForm = c, this.init() }, a.validator.format = function (b, c) { return 1 === arguments.length ? function () { var c = a.makeArray(arguments); return c.unshift(b), a.validator.format.apply(this, c) } : void 0 === c ? b : (arguments.length > 2 && c.constructor !== Array && (c = a.makeArray(arguments).slice(1)), c.constructor !== Array && (c = [c]), a.each(c, function (a, c) { b = b.replace(new RegExp("\\{" + a + "\\}", "g"), function () { return c }) }), b) }, a.extend(a.validator, { defaults: { messages: {}, groups: {}, rules: {}, errorClass: "error", pendingClass: "pending", validClass: "valid", errorElement: "label", focusCleanup: !1, focusInvalid: !0, errorContainer: a([]), errorLabelContainer: a([]), onsubmit: !0, ignore: ":hidden", ignoreTitle: !1, onfocusin: function (a) { this.lastActive = a, this.settings.focusCleanup && (this.settings.unhighlight && this.settings.unhighlight.call(this, a, this.settings.errorClass, this.settings.validClass), this.hideThese(this.errorsFor(a))) }, onfocusout: function (a) { this.checkable(a) || !(a.name in this.submitted) && this.optional(a) || this.element(a) }, onkeyup: function (b, c) { var d = [16, 17, 18, 20, 35, 36, 37, 38, 39, 40, 45, 144, 225]; 9 === c.which && "" === this.elementValue(b) || a.inArray(c.keyCode, d) !== -1 || (b.name in this.submitted || b.name in this.invalid) && this.element(b) }, onclick: function (a) { a.name in this.submitted ? this.element(a) : a.parentNode.name in this.submitted && this.element(a.parentNode) }, highlight: function (b, c, d) { "radio" === b.type ? this.findByName(b.name).addClass(c).removeClass(d) : a(b).addClass(c).removeClass(d) }, unhighlight: function (b, c, d) { "radio" === b.type ? this.findByName(b.name).removeClass(c).addClass(d) : a(b).removeClass(c).addClass(d) } }, setDefaults: function (b) { a.extend(a.validator.defaults, b) }, messages: { required: "This field is required.", remote: "Please fix this field.", email: "Please enter a valid email address.", url: "Please enter a valid URL.", date: "Please enter a valid date.", dateISO: "Please enter a valid date (ISO).", number: "Please enter a valid number.", digits: "Please enter only digits.", equalTo: "Please enter the same value again.", maxlength: a.validator.format("Please enter no more than {0} characters."), minlength: a.validator.format("Please enter at least {0} characters."), rangelength: a.validator.format("Please enter a value between {0} and {1} characters long."), range: a.validator.format("Please enter a value between {0} and {1}."), max: a.validator.format("Please enter a value less than or equal to {0}."), min: a.validator.format("Please enter a value greater than or equal to {0}."), step: a.validator.format("Please enter a multiple of {0}.") }, autoCreateRanges: !1, prototype: { init: function () { function b(b) { !this.form && this.hasAttribute("contenteditable") && (this.form = a(this).closest("form")[0], this.name = a(this).attr("name")); var c = a.data(this.form, "validator"), d = "on" + b.type.replace(/^validate/, ""), e = c.settings; e[d] && !a(this).is(e.ignore) && e[d].call(c, this, b) } this.labelContainer = a(this.settings.errorLabelContainer), this.errorContext = this.labelContainer.length && this.labelContainer || a(this.currentForm), this.containers = a(this.settings.errorContainer).add(this.settings.errorLabelContainer), this.submitted = {}, this.valueCache = {}, this.pendingRequest = 0, this.pending = {}, this.invalid = {}, this.reset(); var c, d = this.groups = {}; a.each(this.settings.groups, function (b, c) { "string" == typeof c && (c = c.split(/\s/)), a.each(c, function (a, c) { d[c] = b }) }), c = this.settings.rules, a.each(c, function (b, d) { c[b] = a.validator.normalizeRule(d) }), a(this.currentForm).on("focusin.validate focusout.validate keyup.validate", ":text, [type='password'], [type='file'], select, textarea, [type='number'], [type='search'], [type='tel'], [type='url'], [type='email'], [type='datetime'], [type='date'], [type='month'], [type='week'], [type='time'], [type='datetime-local'], [type='range'], [type='color'], [type='radio'], [type='checkbox'], [contenteditable], [type='button']", b).on("click.validate", "select, option, [type='radio'], [type='checkbox']", b), this.settings.invalidHandler && a(this.currentForm).on("invalid-form.validate", this.settings.invalidHandler) }, form: function () { return this.checkForm(), a.extend(this.submitted, this.errorMap), this.invalid = a.extend({}, this.errorMap), this.valid() || a(this.currentForm).triggerHandler("invalid-form", [this]), this.showErrors(), this.valid() }, checkForm: function () { this.prepareForm(); for (var a = 0, b = this.currentElements = this.elements(); b[a]; a++)this.check(b[a]); return this.valid() }, element: function (b) { var c, d, e = this.clean(b), f = this.validationTargetFor(e), g = this, h = !0; return void 0 === f ? delete this.invalid[e.name] : (this.prepareElement(f), this.currentElements = a(f), d = this.groups[f.name], d && a.each(this.groups, function (a, b) { b === d && a !== f.name && (e = g.validationTargetFor(g.clean(g.findByName(a))), e && e.name in g.invalid && (g.currentElements.push(e), h = g.check(e) && h)) }), c = this.check(f) !== !1, h = h && c, c ? this.invalid[f.name] = !1 : this.invalid[f.name] = !0, this.numberOfInvalids() || (this.toHide = this.toHide.add(this.containers)), this.showErrors(), a(b).attr("aria-invalid", !c)), h }, showErrors: function (b) { if (b) { var c = this; a.extend(this.errorMap, b), this.errorList = a.map(this.errorMap, function (a, b) { return { message: a, element: c.findByName(b)[0] } }), this.successList = a.grep(this.successList, function (a) { return !(a.name in b) }) } this.settings.showErrors ? this.settings.showErrors.call(this, this.errorMap, this.errorList) : this.defaultShowErrors() }, resetForm: function () { a.fn.resetForm && a(this.currentForm).resetForm(), this.invalid = {}, this.submitted = {}, this.prepareForm(), this.hideErrors(); var b = this.elements().removeData("previousValue").removeAttr("aria-invalid"); this.resetElements(b) }, resetElements: function (a) { var b; if (this.settings.unhighlight) for (b = 0; a[b]; b++)this.settings.unhighlight.call(this, a[b], this.settings.errorClass, ""), this.findByName(a[b].name).removeClass(this.settings.validClass); else a.removeClass(this.settings.errorClass).removeClass(this.settings.validClass) }, numberOfInvalids: function () { return this.objectLength(this.invalid) }, objectLength: function (a) { var b, c = 0; for (b in a) void 0 !== a[b] && null !== a[b] && a[b] !== !1 && c++; return c }, hideErrors: function () { this.hideThese(this.toHide) }, hideThese: function (a) { a.not(this.containers).text(""), this.addWrapper(a).hide() }, valid: function () { return 0 === this.size() }, size: function () { return this.errorList.length }, focusInvalid: function () { if (this.settings.focusInvalid) try { a(this.findLastActive() || this.errorList.length && this.errorList[0].element || []).filter(":visible").focus().trigger("focusin") } catch (b) { } }, findLastActive: function () { var b = this.lastActive; return b && 1 === a.grep(this.errorList, function (a) { return a.element.name === b.name }).length && b }, elements: function () { var b = this, c = {}; return a(this.currentForm).find("input, select, textarea, [contenteditable]").not(":submit, :reset, :image, :disabled").not(this.settings.ignore).filter(function () { var d = this.name || a(this).attr("name"); return !d && b.settings.debug && window.console && console.error("%o has no name assigned", this), this.hasAttribute("contenteditable") && (this.form = a(this).closest("form")[0], this.name = d), !(d in c || !b.objectLength(a(this).rules())) && (c[d] = !0, !0) }) }, clean: function (b) { return a(b)[0] }, errors: function () { var b = this.settings.errorClass.split(" ").join("."); return a(this.settings.errorElement + "." + b, this.errorContext) }, resetInternals: function () { this.successList = [], this.errorList = [], this.errorMap = {}, this.toShow = a([]), this.toHide = a([]) }, reset: function () { this.resetInternals(), this.currentElements = a([]) }, prepareForm: function () { this.reset(), this.toHide = this.errors().add(this.containers) }, prepareElement: function (a) { this.reset(), this.toHide = this.errorsFor(a) }, elementValue: function (b) { var c, d, e = a(b), f = b.type; return "radio" === f || "checkbox" === f ? this.findByName(b.name).filter(":checked").val() : "number" === f && "undefined" != typeof b.validity ? b.validity.badInput ? "NaN" : e.val() : (c = b.hasAttribute("contenteditable") ? e.text() : e.val(), "file" === f ? "C:\\fakepath\\" === c.substr(0, 12) ? c.substr(12) : (d = c.lastIndexOf("/"), d >= 0 ? c.substr(d + 1) : (d = c.lastIndexOf("\\"), d >= 0 ? c.substr(d + 1) : c)) : "string" == typeof c ? c.replace(/\r/g, "") : c) }, check: function (b) { b = this.validationTargetFor(this.clean(b)); var c, d, e, f, g = a(b).rules(), h = a.map(g, function (a, b) { return b }).length, i = !1, j = this.elementValue(b); if ("function" == typeof g.normalizer ? f = g.normalizer : "function" == typeof this.settings.normalizer && (f = this.settings.normalizer), f) { if (j = f.call(b, j), "string" != typeof j) throw new TypeError("The normalizer should return a string value."); delete g.normalizer } for (d in g) { e = { method: d, parameters: g[d] }; try { if (c = a.validator.methods[d].call(this, j, b, e.parameters), "dependency-mismatch" === c && 1 === h) { i = !0; continue } if (i = !1, "pending" === c) return void (this.toHide = this.toHide.not(this.errorsFor(b))); if (!c) return this.formatAndAdd(b, e), !1 } catch (k) { throw this.settings.debug && window.console && console.log("Exception occurred when checking element " + b.id + ", check the '" + e.method + "' method.", k), k instanceof TypeError && (k.message += ".  Exception occurred when checking element " + b.id + ", check the '" + e.method + "' method."), k } } if (!i) return this.objectLength(g) && this.successList.push(b), !0 }, customDataMessage: function (b, c) { return a(b).data("msg" + c.charAt(0).toUpperCase() + c.substring(1).toLowerCase()) || a(b).data("msg") }, customMessage: function (a, b) { var c = this.settings.messages[a]; return c && (c.constructor === String ? c : c[b]) }, findDefined: function () { for (var a = 0; a < arguments.length; a++)if (void 0 !== arguments[a]) return arguments[a] }, defaultMessage: function (b, c) { "string" == typeof c && (c = { method: c }); var d = this.findDefined(this.customMessage(b.name, c.method), this.customDataMessage(b, c.method), !this.settings.ignoreTitle && b.title || void 0, a.validator.messages[c.method], "<strong>Warning: No message defined for " + b.name + "</strong>"), e = /\$?\{(\d+)\}/g; return "function" == typeof d ? d = d.call(this, c.parameters, b) : e.test(d) && (d = a.validator.format(d.replace(e, "{$1}"), c.parameters)), d }, formatAndAdd: function (a, b) { var c = this.defaultMessage(a, b); this.errorList.push({ message: c, element: a, method: b.method }), this.errorMap[a.name] = c, this.submitted[a.name] = c }, addWrapper: function (a) { return this.settings.wrapper && (a = a.add(a.parent(this.settings.wrapper))), a }, defaultShowErrors: function () { var a, b, c; for (a = 0; this.errorList[a]; a++)c = this.errorList[a], this.settings.highlight && this.settings.highlight.call(this, c.element, this.settings.errorClass, this.settings.validClass), this.showLabel(c.element, c.message); if (this.errorList.length && (this.toShow = this.toShow.add(this.containers)), this.settings.success) for (a = 0; this.successList[a]; a++)this.showLabel(this.successList[a]); if (this.settings.unhighlight) for (a = 0, b = this.validElements(); b[a]; a++)this.settings.unhighlight.call(this, b[a], this.settings.errorClass, this.settings.validClass); this.toHide = this.toHide.not(this.toShow), this.hideErrors(), this.addWrapper(this.toShow).show() }, validElements: function () { return this.currentElements.not(this.invalidElements()) }, invalidElements: function () { return a(this.errorList).map(function () { return this.element }) }, showLabel: function (b, c) { var d, e, f, g, h = this.errorsFor(b), i = this.idOrName(b), j = a(b).attr("aria-describedby"); h.length ? (h.removeClass(this.settings.validClass).addClass(this.settings.errorClass), h.html(c)) : (h = a("<" + this.settings.errorElement + ">").attr("id", i + "-error").addClass(this.settings.errorClass).html(c || ""), d = h, this.settings.wrapper && (d = h.hide().show().wrap("<" + this.settings.wrapper + "/>").parent()), this.labelContainer.length ? this.labelContainer.append(d) : this.settings.errorPlacement ? this.settings.errorPlacement.call(this, d, a(b)) : d.insertAfter(b), h.is("label") ? h.attr("for", i) : 0 === h.parents("label[for='" + this.escapeCssMeta(i) + "']").length && (f = h.attr("id"), j ? j.match(new RegExp("\\b" + this.escapeCssMeta(f) + "\\b")) || (j += " " + f) : j = f, a(b).attr("aria-describedby", j), e = this.groups[b.name], e && (g = this, a.each(g.groups, function (b, c) { c === e && a("[name='" + g.escapeCssMeta(b) + "']", g.currentForm).attr("aria-describedby", h.attr("id")) })))), !c && this.settings.success && (h.text(""), "string" == typeof this.settings.success ? h.addClass(this.settings.success) : this.settings.success(h, b)), this.toShow = this.toShow.add(h) }, errorsFor: function (b) { var c = this.escapeCssMeta(this.idOrName(b)), d = a(b).attr("aria-describedby"), e = "label[for='" + c + "'], label[for='" + c + "'] *"; return d && (e = e + ", #" + this.escapeCssMeta(d).replace(/\s+/g, ", #")), this.errors().filter(e) }, escapeCssMeta: function (a) { return a.replace(/([\\!"#$%&'()*+,.\/:;<=>?@\[\]^`{|}~])/g, "\\$1") }, idOrName: function (a) { return this.groups[a.name] || (this.checkable(a) ? a.name : a.id || a.name) }, validationTargetFor: function (b) { return this.checkable(b) && (b = this.findByName(b.name)), a(b).not(this.settings.ignore)[0] }, checkable: function (a) { return /radio|checkbox/i.test(a.type) }, findByName: function (b) { return a(this.currentForm).find("[name='" + this.escapeCssMeta(b) + "']") }, getLength: function (b, c) { switch (c.nodeName.toLowerCase()) { case "select": return a("option:selected", c).length; case "input": if (this.checkable(c)) return this.findByName(c.name).filter(":checked").length }return b.length }, depend: function (a, b) { return !this.dependTypes[typeof a] || this.dependTypes[typeof a](a, b) }, dependTypes: { "boolean": function (a) { return a }, string: function (b, c) { return !!a(b, c.form).length }, "function": function (a, b) { return a(b) } }, optional: function (b) { var c = this.elementValue(b); return !a.validator.methods.required.call(this, c, b) && "dependency-mismatch" }, startRequest: function (b) { this.pending[b.name] || (this.pendingRequest++, a(b).addClass(this.settings.pendingClass), this.pending[b.name] = !0) }, stopRequest: function (b, c) { this.pendingRequest--, this.pendingRequest < 0 && (this.pendingRequest = 0), delete this.pending[b.name], a(b).removeClass(this.settings.pendingClass), c && 0 === this.pendingRequest && this.formSubmitted && this.form() ? (a(this.currentForm).submit(), this.submitButton && a("input:hidden[name='" + this.submitButton.name + "']", this.currentForm).remove(), this.formSubmitted = !1) : !c && 0 === this.pendingRequest && this.formSubmitted && (a(this.currentForm).triggerHandler("invalid-form", [this]), this.formSubmitted = !1) }, previousValue: function (b, c) { return c = "string" == typeof c && c || "remote", a.data(b, "previousValue") || a.data(b, "previousValue", { old: null, valid: !0, message: this.defaultMessage(b, { method: c }) }) }, destroy: function () { this.resetForm(), a(this.currentForm).off(".validate").removeData("validator").find(".validate-equalTo-blur").off(".validate-equalTo").removeClass("validate-equalTo-blur") } }, classRuleSettings: { required: { required: !0 }, email: { email: !0 }, url: { url: !0 }, date: { date: !0 }, dateISO: { dateISO: !0 }, number: { number: !0 }, digits: { digits: !0 }, creditcard: { creditcard: !0 } }, addClassRules: function (b, c) { b.constructor === String ? this.classRuleSettings[b] = c : a.extend(this.classRuleSettings, b) }, classRules: function (b) { var c = {}, d = a(b).attr("class"); return d && a.each(d.split(" "), function () { this in a.validator.classRuleSettings && a.extend(c, a.validator.classRuleSettings[this]) }), c }, normalizeAttributeRule: function (a, b, c, d) { /min|max|step/.test(c) && (null === b || /number|range|text/.test(b)) && (d = Number(d), isNaN(d) && (d = void 0)), d || 0 === d ? a[c] = d : b === c && "range" !== b && (a[c] = !0) }, attributeRules: function (b) { var c, d, e = {}, f = a(b), g = b.getAttribute("type"); for (c in a.validator.methods) "required" === c ? (d = b.getAttribute(c), "" === d && (d = !0), d = !!d) : d = f.attr(c), this.normalizeAttributeRule(e, g, c, d); return e.maxlength && /-1|2147483647|524288/.test(e.maxlength) && delete e.maxlength, e }, dataRules: function (b) { var c, d, e = {}, f = a(b), g = b.getAttribute("type"); for (c in a.validator.methods) d = f.data("rule" + c.charAt(0).toUpperCase() + c.substring(1).toLowerCase()), this.normalizeAttributeRule(e, g, c, d); return e }, staticRules: function (b) { var c = {}, d = a.data(b.form, "validator"); return d.settings.rules && (c = a.validator.normalizeRule(d.settings.rules[b.name]) || {}), c }, normalizeRules: function (b, c) { return a.each(b, function (d, e) { if (e === !1) return void delete b[d]; if (e.param || e.depends) { var f = !0; switch (typeof e.depends) { case "string": f = !!a(e.depends, c.form).length; break; case "function": f = e.depends.call(c, c) }f ? b[d] = void 0 === e.param || e.param : (a.data(c.form, "validator").resetElements(a(c)), delete b[d]) } }), a.each(b, function (d, e) { b[d] = a.isFunction(e) && "normalizer" !== d ? e(c) : e }), a.each(["minlength", "maxlength"], function () { b[this] && (b[this] = Number(b[this])) }), a.each(["rangelength", "range"], function () { var c; b[this] && (a.isArray(b[this]) ? b[this] = [Number(b[this][0]), Number(b[this][1])] : "string" == typeof b[this] && (c = b[this].replace(/[\[\]]/g, "").split(/[\s,]+/), b[this] = [Number(c[0]), Number(c[1])])) }), a.validator.autoCreateRanges && (null != b.min && null != b.max && (b.range = [b.min, b.max], delete b.min, delete b.max), null != b.minlength && null != b.maxlength && (b.rangelength = [b.minlength, b.maxlength], delete b.minlength, delete b.maxlength)), b }, normalizeRule: function (b) { if ("string" == typeof b) { var c = {}; a.each(b.split(/\s/), function () { c[this] = !0 }), b = c } return b }, addMethod: function (b, c, d) { a.validator.methods[b] = c, a.validator.messages[b] = void 0 !== d ? d : a.validator.messages[b], c.length < 3 && a.validator.addClassRules(b, a.validator.normalizeRule(b)) }, methods: { required: function (b, c, d) { if (!this.depend(d, c)) return "dependency-mismatch"; if ("select" === c.nodeName.toLowerCase()) { var e = a(c).val(); return e && e.length > 0 } return this.checkable(c) ? this.getLength(b, c) > 0 : b.length > 0 }, email: function (a, b) { return this.optional(b) || /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/.test(a) }, url: function (a, b) { return this.optional(b) || /^(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})).?)(?::\d{2,5})?(?:[\/?#]\S*)?$/i.test(a) }, date: function (a, b) { return this.optional(b) || !/Invalid|NaN/.test(new Date(a).toString()) }, dateISO: function (a, b) { return this.optional(b) || /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/.test(a) }, number: function (a, b) { return this.optional(b) || /^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(a) }, digits: function (a, b) { return this.optional(b) || /^\d+$/.test(a) }, minlength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || e >= d }, maxlength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || e <= d }, rangelength: function (b, c, d) { var e = a.isArray(b) ? b.length : this.getLength(b, c); return this.optional(c) || e >= d[0] && e <= d[1] }, min: function (a, b, c) { return this.optional(b) || a >= c }, max: function (a, b, c) { return this.optional(b) || a <= c }, range: function (a, b, c) { return this.optional(b) || a >= c[0] && a <= c[1] }, step: function (b, c, d) { var e, f = a(c).attr("type"), g = "Step attribute on input type " + f + " is not supported.", h = ["text", "number", "range"], i = new RegExp("\\b" + f + "\\b"), j = f && !i.test(h.join()), k = function (a) { var b = ("" + a).match(/(?:\.(\d+))?$/); return b && b[1] ? b[1].length : 0 }, l = function (a) { return Math.round(a * Math.pow(10, e)) }, m = !0; if (j) throw new Error(g); return e = k(d), (k(b) > e || l(b) % l(d) !== 0) && (m = !1), this.optional(c) || m }, equalTo: function (b, c, d) { var e = a(d); return this.settings.onfocusout && e.not(".validate-equalTo-blur").length && e.addClass("validate-equalTo-blur").on("blur.validate-equalTo", function () { a(c).valid() }), b === e.val() }, remote: function (b, c, d, e) { if (this.optional(c)) return "dependency-mismatch"; e = "string" == typeof e && e || "remote"; var f, g, h, i = this.previousValue(c, e); return this.settings.messages[c.name] || (this.settings.messages[c.name] = {}), i.originalMessage = i.originalMessage || this.settings.messages[c.name][e], this.settings.messages[c.name][e] = i.message, d = "string" == typeof d && { url: d } || d, h = a.param(a.extend({ data: b }, d.data)), i.old === h ? i.valid : (i.old = h, f = this, this.startRequest(c), g = {}, g[c.name] = b, a.ajax(a.extend(!0, { mode: "abort", port: "validate" + c.name, dataType: "json", data: g, context: f.currentForm, success: function (a) { var d, g, h, j = a === !0 || "true" === a; f.settings.messages[c.name][e] = i.originalMessage, j ? (h = f.formSubmitted, f.resetInternals(), f.toHide = f.errorsFor(c), f.formSubmitted = h, f.successList.push(c), f.invalid[c.name] = !1, f.showErrors()) : (d = {}, g = a || f.defaultMessage(c, { method: e, parameters: b }), d[c.name] = i.message = g, f.invalid[c.name] = !0, f.showErrors(d)), i.valid = j, f.stopRequest(c, j) } }, d)), "pending") } } }); var b, c = {}; return a.ajaxPrefilter ? a.ajaxPrefilter(function (a, b, d) { var e = a.port; "abort" === a.mode && (c[e] && c[e].abort(), c[e] = d) }) : (b = a.ajax, a.ajax = function (d) { var e = ("mode" in d ? d : a.ajaxSettings).mode, f = ("port" in d ? d : a.ajaxSettings).port; return "abort" === e ? (c[f] && c[f].abort(), c[f] = b.apply(this, arguments), c[f]) : b.apply(this, arguments) }), a });
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
});