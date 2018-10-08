<template>
    <v-dialog v-model="createKeep" transition="slide-y-transition">
        <v-btn slot="activator" flat fab>
            <v-icon>fas fa-pencil-alt</v-icon>
        </v-btn>
        <v-card>
            <v-toolbar card color="amber darken 4">
                <v-btn @click="createKeep = false" fab>
                    <v-icon>close</v-icon>
                </v-btn>
            </v-toolbar>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
    name: 'createKeep',
    data() {
        return {
            createKeep: false
        }
    }
}
</script>
