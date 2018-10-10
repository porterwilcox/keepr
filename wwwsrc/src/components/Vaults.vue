<template>
    <div>
        <v-card v-if="!(i % 3)" color="light-blue darken-3" width="20vw" height="10vw">
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>test</h2>
            </v-layout>
        </v-card>
        <v-card v-else-if="!(i % 2)" color="cyan darken-3" width="20vw" height="10vw">
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>test</h2>
            </v-layout>        </v-card>
        <v-card v-else color="teal darken-3" width="20vw" height="10vw">
            <v-layout fill-height column align-center justify-space-around>
                <h1 class="white--text">{{vault.name}}</h1>
                <h2>test</h2>
            </v-layout>
        </v-card>
    </div>
</template>
<script>
export default {
    name: 'vaults',
    props: ["vault", "i"]
}
</script>
