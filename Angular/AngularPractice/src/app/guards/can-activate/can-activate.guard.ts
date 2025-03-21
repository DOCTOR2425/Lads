import { CanActivateFn } from '@angular/router';

export const canActivateGuard: CanActivateFn = (route, state) => {
  const currentHour = new Date().getHours();
  console.log(currentHour);

  return true;
  if (currentHour >= 9 && currentHour < 17) {
  } else {
    alert('Доступ к этому маршруту разрешен только во время с 9:00 до 17:00.');
    return false;
  }
};
