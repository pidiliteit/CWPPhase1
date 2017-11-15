app.directive('autocomplete', function ($http, $interpolate, $parse) {
    return {
        restrict: 'E',
        replace: true,
        template: '<input style="display:;" type="text"/>',
        //        template: '<div ><label></label><input style="display:none" type="text"/></div>',
        require: 'ngModel',
        // require: 'ngBind',
        compile: function (elem, attrs) {
            //            alert('compile')
            var modelAccessor = $parse(attrs.ngModel),
                labelExpression = attrs.label;
            //console.log($parse(attrs.ngModel))
            return function (scope, element, attrs, controller) {
                //    alert()
                controller.$formatters.push(function (value) {
                    //alert($interpolate(labelExpression)(value))

                    if (!value) return ''
                    else return $interpolate(labelExpression)(value)
                })
                //var lb = element.children('label');
                //var tb = element.children('input');
                //  console.log(modelAccessor.data)
                //alert(element.val());
                //element.bind('click', function () { alert(element.val())})
                mappedItems = null,
                allowCustomEntry = attrs.allowCustomEntry || false;
                //alert(element.val())
                element.autocomplete({
                    source: function (request, response) {
                        var strquery = JSON.stringify({ CurrentPage: 0, PageSize: 100, Parameters: [{ name: "AutoComplete", value: request.term }] });
                        var mth = 'GET';
                        alert(attrs.url);
                        if (attrs.url == "/api/PendingEmp") {
                            mth = 'Post';
                        }
                        else if (attrs.url == "/api/userHelper") {
                            mth = 'Post';
                        }

                        console.log(mth);
                        $http({
                            url: attrs.url,
                            method: mth,
                            params: { query: strquery }
                        })
                        .success(function (data) {
                            console.log(data);
                            var fileredData = [];
                            var dataColumn = attrs.dataformcolumn;
                            for (i in data.items) {
                                fileredData.push(data.items[i])//[dataColumn]);
                            }
                            //  console.log(fileredData);


                            mappedItems = $.map(fileredData, function (item) {
                                var result = {};

                                if (typeof item === "string") {
                                    result.label = item;
                                    result.value = item;

                                    return result;
                                }

                                result.label = $interpolate(labelExpression)(item);//item[labelExpression];//

                                if (attrs.value) {
                                    result.value = item[attrs.value];
                                }
                                else {
                                    result.value = item;
                                }

                                return result;
                            });

                            return response(mappedItems);
                        });
                    },

                    select: function (event, ui) {
                        scope.$apply(function (scope) {
                            modelAccessor.assign(scope, ui.item.value);
                        });
                        //alert(elem.val())
                        //elem.val("aaaa");
                        //alert('hii')

                        event.preventDefault();
                    },

                    change: function (event, ui) {
                        var


                            currentValue = elem.val(),
                            matchingItem = null;
                        //alert();

                        if (allowCustomEntry) {
                            return;
                        }

                        for (var i = 0; i < mappedItems.length; i++) {
                            if (mappedItems[i].label === currentValue.label) {
                                console.log(mappedItems[i].label)
                                matchingItem = mappedItems[i].label;
                                break;
                            }
                        }

                        if (!matchingItem) {
                            scope.$apply(function (scope) {
                                //      modelAccessor.assign(scope, null);
                            });
                        }
                    }
                });
            }
        }
    }
});
