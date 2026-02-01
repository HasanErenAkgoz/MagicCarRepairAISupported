import { apiPost } from './http';
import { saveAuthSession } from './storage';

function extractTokenAndUser(loginResponse) {
  // Be defensive: backend response shape might vary.
  // Common shapes:
  // 1) { token, user }
  // 2) { data: { token, user } }
  // 3) { success: true, data: { token: { token: '...', ... } } }
  const root = loginResponse || {};
  const data = root.data ?? root;

  // token candidates
  const token =
    data?.token?.token ||
    data?.token?.Token ||
    data?.token ||
    data?.Token ||
    data?.accessToken ||
    data?.AccessToken ||
    null;

  const user = data?.user || data?.User || root?.user || null;
  return { token, user };
}

export async function login(email, password) {
  const payload = { email, password };
  const res = await apiPost('/Auth/login', payload);
  const { token, user } = extractTokenAndUser(res);

  if (!token) {
    // still store raw for debugging
    await saveAuthSession({ token: null, user: user || null, raw: res });
    throw new Error('Login başarılı ama token bulunamadı (response formatı beklenenden farklı).');
  }

  await saveAuthSession({ token, user: user || null, raw: res });
  return { token, user, raw: res };
}

export async function register(data) {
  const payload = {
    firstName: data.firstName,
    lastName: data.lastName,
    identityNo: data.identityNo,
    phoneNumber: data.phoneNumber,
    address: data.address,
    email: data.email,
    password: data.password,
    confirmPassword: data.confirmPassword,
  };
  const res = await apiPost('/Auth/register', payload);
  const { token, user } = extractTokenAndUser(res);

  if (token) {
    await saveAuthSession({ token, user: user || null, raw: res });
  }

  return { token, user, raw: res };
}
