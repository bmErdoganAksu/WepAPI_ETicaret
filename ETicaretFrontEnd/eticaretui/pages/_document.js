import { Html, Head, Main, NextScript } from 'next/document'

export default function Document() {
    return (
        <Html lang="tr">
            <Head >
                <link rel="preconnect" href="https://fonts.googleapis.com" />
                <link rel="preconnect" href="https://fonts.gstatic.com" crossOrigin="true" />
                <link href="https://fonts.googleapis.com/css2?family=Rubik:wght@300;400;500;600;700;800&display=swap" rel="stylesheet" />
                <meta name="viewport" content="width=device-width, initial-scale=1" />
            </Head>
            <body>
                <Main />
                <NextScript />
            </body>
        </Html>
    )
}