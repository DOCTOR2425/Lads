self.addEventListener('install', function(event) {
  event.waitUntil(
    caches.open('my-cache').then(function(cache) {
      return cache.addAll([
        '/',
        '/index.html',
        '/styles.css',
        '/main.js'
      ]);
    })
  );
});

self.addEventListener('fetch', function(event) {
  event.respondWith(
    caches.match(event.request).then(function(cachedResponse) {
      const fetchPromise = fetch(event.request).then(function(response) {
        // Клонируем ответ, чтобы использовать его для кэша и возвращения пользователю
        const responseClone = response.clone();
        caches.open('my-cache').then(function(cache) {
          cache.put(event.request, responseClone);
        });

        // Отправляем обновленные данные всем клиентам
        responseClone.json().then(function(data) {
          clients.matchAll().then(clients => {
            clients.forEach(client => {
              client.postMessage({ type: 'UPDATE_RESPONSE', data: data });
            });
          });
        });

        return response;
      }).catch(function() {
        console.log('Failed to fetch from server:', event.request.url);
      });

      // Возвращаем кэшированные данные немедленно (если они есть) и затем данные от сервера
      return cachedResponse || fetchPromise;
    })
  );
});

self.addEventListener('message', function(event) {
  if (event.data && event.data.type === 'SKIP_WAITING') {
    self.skipWaiting();
  }
});

// self.addEventListener('fetch', function(event) {
//   console.log('Fetching:', event.request.url);

//   event.respondWith(
//     fetch(event.request)
//       .then(function(response) {
//         console.log('Response from server:', event.request.url);
//         const responseClone = response.clone();
//         caches.open('my-cache').then(function(cache) {
//           cache.put(event.request, responseClone);
//         });
//         return response;
//       })
//       .catch(function() {
//         console.log('Fetch failed, trying cache:', event.request.url);
//         return caches.match(event.request).then(function(cachedResponse) {
//           if (cachedResponse) {
//             console.log('Response from cache:', event.request.url);
//             return cachedResponse;
//           }
//           throw new Error('Network error and no cached data available');
//         });
//       })
//   );
// });
