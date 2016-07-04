'use strict';
angular.module('netMQToastNotifications', [])
.run(function() {
        toastr.options = {
            "closeButton": true,
            "positionClass": "toast-bottom-right"
        }

        var subscriber = new JSMQ.Subscriber();
        
        subscriber.connect("ws://localhost:81");
        subscriber.subscribe("toast");

        subscriber.onMessage = function (message) {
            message.popString();
            toastr.info(message.popString());
        };
    })