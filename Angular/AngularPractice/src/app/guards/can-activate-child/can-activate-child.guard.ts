import { CanActivateChildFn } from '@angular/router';

export const canActivateChildGuard: CanActivateChildFn = (
  childRoute,
  state
) => {
  const currentHour = new Date().getHours();
  if (childRoute.routeConfig?.path == 'payment') {
    console.log(currentHour);
    if (currentHour >= 9 && currentHour < 17) {
      return true;
    } else {
      alert(
        'Доступ к этому маршруту разрешен только во время с 9:00 до 17:00.'
      );
      return false;
    }
  } else return true;
};
