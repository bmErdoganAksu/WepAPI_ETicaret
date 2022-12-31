import { Toaster } from 'react-hot-toast';
import { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.css'
import '../styles/globals.css'
import HomeLayout from '../layouts/HomeLayout';

export default function ETicaretApp({ Component, pageProps }) {
  const [domLoaded, setDomLoaded] = useState(false);
  useEffect(() => { setDomLoaded(true) }, [])
  const getLayout = Component.getLayout || ((page) => page)

  return getLayout(
    <HomeLayout>
      <Component {...pageProps} />
      {domLoaded && <Toaster />}
    </HomeLayout>
  )

}

