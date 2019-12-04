//
//  IOSNativeAlert.m
//  Alert
//
//  Created by Nrjwolf on 04/12/2019.
//  Copyright © 2019 Nrjwolf. All rights reserved.
//

extern UIViewController *UnityGetGLViewController();

NSString *ToNSString(char* string) {
    return [NSString stringWithUTF8String:string];
}

/// Alert function
void _IOSShowAlertMsg (char* title, char* message) {
    UIAlertController * alert = [UIAlertController alertControllerWithTitle : ToNSString(title)
                                                                    message : ToNSString(message)
                                                             preferredStyle : UIAlertControllerStyleAlert];

    UIAlertAction * ok = [UIAlertAction
                          actionWithTitle:@"OK"
                          style:UIAlertActionStyleDefault
                          handler:^(UIAlertAction * action)
                          { }];

    [alert addAction:ok];
    dispatch_async(dispatch_get_main_queue(), ^{
        [UnityGetGLViewController() presentViewController:alert animated:YES completion:nil];
    });

}

/// Show something like android toast
void _IOSShowToast (char* message, BOOL isLongDuration) {
    NSString *messageString = ToNSString(message);
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:messageString message:@"" preferredStyle:UIAlertControllerStyleAlert];
    
    UIWindow *alertWindow = [[UIWindow alloc] initWithFrame:[UIScreen mainScreen].bounds];
    alertWindow.rootViewController = [[UIViewController alloc] init];
    alertWindow.windowLevel = UIWindowLevelAlert + 1;
    [alertWindow makeKeyAndVisible];
    [alertWindow.rootViewController presentViewController:alert animated:YES completion:nil];
    
    float showDuration = isLongDuration ? 1 : .5;
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(showDuration * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        [alertWindow.rootViewController dismissViewControllerAnimated:YES completion:nil];
    });
}
